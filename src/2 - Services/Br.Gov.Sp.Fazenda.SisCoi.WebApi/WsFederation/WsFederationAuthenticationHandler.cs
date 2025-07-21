using Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation.Events;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.WsFederation;
using Microsoft.IdentityModel.Tokens;
using Sefaz.Identity.SefazIdentityTokenValidation;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Xml;
using static Microsoft.IdentityModel.Protocols.WsFederation.WsFederationConstants;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation
{
    public class WsFederationAuthenticationHandler : RemoteAuthenticationHandler<WsFederationAuthenticationOptions>
    {
        private readonly IConfiguration _configMachine;
        private readonly IOptionsMonitor<WsFederationAuthenticationOptions> _options;
        private readonly ILoggerFactory _loggerFactory;
        IDataProtectionProvider _dataProtectionProvider;
        private readonly UrlEncoder _encoder;
        private readonly ISystemClock _clock;
        private WsFederationConfiguration _configuration;

        public WsFederationAuthenticationHandler(IOptionsMonitor<WsFederationAuthenticationOptions> options,
             IDataProtectionProvider dataProtectionProvider,
             ILoggerFactory logger,
             IConfiguration config,
             UrlEncoder encoder,
             ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _options = options;
            _configMachine = config;
            _loggerFactory = logger;
            _encoder = encoder;
            _clock = clock;
            _dataProtectionProvider = dataProtectionProvider;

            if (_options.CurrentValue.StateDataFormat == null)
            {
                var dataProtector = dataProtectionProvider.CreateProtector("v1");
                _options.CurrentValue.StateDataFormat = new PropertiesDataFormat(dataProtector);
            }
        }

        protected override async Task<bool> HandleChallengeAsync(AuthenticationProperties properties)
        {
            if (Context == null)
            {
                throw new ArgumentNullException(nameof(Context));
            }

            if (_configuration == null)
            {
                IConfigurationRetriever<WsFederationConfiguration> _configurationRetriever = new WsFederationConfigurationRetriever();
                Options.ConfigurationManager = new Microsoft.IdentityModel.Protocols.ConfigurationManager<WsFederationConfiguration>(_configMachine["SefazIdentity:MetadataAddress"], _configurationRetriever);
                _configuration = Options.ConfigurationManager.GetConfigurationAsync(Context.RequestAborted).Result;
            }

            var baseUri =
                Request.Scheme +
                Uri.SchemeDelimiter +
                Request.Host +
                Request.PathBase;

            var currentUri =
                baseUri +
                Request.Path +
                Request.QueryString;

            var wsFederationMessage = new WsFederationMessage
            {
                IssuerAddress = _configuration.TokenEndpoint ?? string.Empty,
                Wtrealm = Options.Wtrealm,
                Wctx = $"{WsFederationAuthenticationDefaults.WctxKey}={Uri.EscapeDataString(_options.CurrentValue.StateDataFormat.Protect(properties))}",
                Wa = WsFederationActions.SignIn,
                Wreply = Options.Wreply,
            };

            if (!string.IsNullOrWhiteSpace(Options.Wreply))
            {
                wsFederationMessage.Wreply = Options.Wreply;
            }

            var AuthenticationScheme = new AuthenticationScheme(WsFederationAuthenticationDefaults.AuthenticationType, "Federation", typeof(AuthenticationHandler<WsFederationAuthenticationOptions>));

            var redirectContext = new RedirectContext(Context, AuthenticationScheme, Options)
            {
                ProtocolMessage = wsFederationMessage,
                Properties = properties
            };

            await Options.Events.RedirectToIdentityProvider(redirectContext, _configMachine);

            if (redirectContext.Response == null)
            {
                Logger.LogDebug("RedirectContext.HandledResponse");
            }

            var redirectUri = redirectContext.ProtocolMessage.CreateSignInUrl();

            Response.Redirect(redirectUri);
            return true;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Context.Response.HttpContext.User != null && Context.Response.HttpContext.User.Identity.IsAuthenticated)
            {
                return HandleRequestResult.SkipHandler();
            }

            WsFederationMessage wsFederationMessage = null;
            var AuthenticationScheme = new AuthenticationScheme(WsFederationAuthenticationDefaults.AuthenticationType, "Federation", typeof(AuthenticationHandler<WsFederationAuthenticationOptions>));

            if (string.Equals(Request.Method, "POST", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(Request.ContentType)
                && Request.ContentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase)
                && Request.Body.CanRead)
            {
                if (!Request.Body.CanSeek)
                {
                    Logger.LogDebug("Buffering request body");
                    var memoryStream = new MemoryStream();
                    await Request.Body.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    Request.Body = memoryStream;
                }
                var form = Request.ReadFormAsync();
                Request.Body.Seek(0, SeekOrigin.Begin);

                wsFederationMessage = new WsFederationMessage(
                    form.Result.Select(pair => new KeyValuePair<string, string[]>(pair.Key, pair.Value.ToArray())));
            }

            try
            {
                if (wsFederationMessage == null || !wsFederationMessage.IsSignInMessage)
                {
                    if (Options.SkipUnrecognizedRequests)
                    {
                        return HandleRequestResult.SkipHandler();
                    }

                    return HandleRequestResult.SkipHandler();
                }

                var messageReceivedContext = await RunMessageReceivedEventAsync(wsFederationMessage, AuthenticationScheme);

                if (messageReceivedContext.Response == null)
                {
                    return HandleRequestResult.SkipHandler();
                }

                if (wsFederationMessage.Wresult == null)
                {
                    return HandleRequestResult.SkipHandler();
                }

                var token = wsFederationMessage.GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return HandleRequestResult.SkipHandler();
                }

                var securityTokenContext = await RunSecurityTokenReceivedEventAsync(wsFederationMessage, AuthenticationScheme);

                if (securityTokenContext.Response == null)
                {
                    return HandleRequestResult.SkipHandler();
                }

                ClaimsPrincipal principal = new ClaimsPrincipal();
                ClaimsIdentity ci2 = SefazIdentityTokenValidation.ExtraiClaimsIdentity(token,
                                                                                   _configMachine["SefazIdentity:Wtrealm"],
                                                                                   _configMachine["SefazIdentity:TipoLogin"],
                                                                                   _configMachine["SefazIdentity:TokenWS"]);
                principal.AddIdentity(ci2);

                // Retornar nova instancia ou cacheado
                var state = wsFederationMessage.Wctx;
                
                var properties = GetPropertiesFromWctx(state);
                properties.RedirectUri = BuildWreply(_configMachine["SefazIdentity:Wreply"]);

                var ticket = new AuthenticationTicket(principal, properties,
                    WsFederationAuthenticationDefaults.AuthenticationType);

                if (Options.UseTokenLifetime)
                {
                    ticket.Properties.IssuedUtc = DateTime.Now.ToUniversalTime();
                    ticket.Properties.ExpiresUtc = DateTime.Now.ToUniversalTime();
                    ticket.Properties.AllowRefresh = false;
                }

                var securityTokenValidatedNotification = await RunSecurityTokenValidatedEventAsync(wsFederationMessage,
                    ticket, AuthenticationScheme);

                return HandleRequestResult.Success(ticket);
            }
            catch (Exception exception)
            {
                var authenticationFailedNotification = await RunAuthenticationFailedEventAsync(wsFederationMessage,
                    exception, AuthenticationScheme);

                return HandleRequestResult.Fail(exception);
            }
        }

        protected override async Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
        {
            if (Context.Response.HttpContext.User != null && Context.Response.HttpContext.User.Identity.IsAuthenticated)
            {
                return HandleRequestResult.SkipHandler();
            }

            WsFederationMessage wsFederationMessage = null;
            var AuthenticationScheme = new AuthenticationScheme(WsFederationAuthenticationDefaults.AuthenticationType, "Federation", typeof(AuthenticationHandler<WsFederationAuthenticationOptions>));

            if (string.Equals(Request.Method, "POST", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(Request.ContentType)
                && Request.ContentType.StartsWith("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase)
                && Request.Body.CanRead)
            {
                if (!Request.Body.CanSeek)
                {
                    Logger.LogDebug("Buffering request body");
                    var memoryStream = new MemoryStream();
                    await Request.Body.CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    Request.Body = memoryStream;
                }
                var form = Request.ReadFormAsync();
                Request.Body.Seek(0, SeekOrigin.Begin);

                wsFederationMessage = new WsFederationMessage(
                    form.Result.Select(pair => new KeyValuePair<string, string[]>(pair.Key, pair.Value.ToArray())));
            }

            try
            {
                if (wsFederationMessage == null || !wsFederationMessage.IsSignInMessage)
                {
                    if (Options.SkipUnrecognizedRequests)
                    {
                        return HandleRequestResult.SkipHandler();
                    }

                    return HandleRequestResult.SkipHandler();
                }

                var messageReceivedContext = await RunMessageReceivedEventAsync(wsFederationMessage, AuthenticationScheme);

                if (messageReceivedContext.Response == null)
                {
                    return HandleRequestResult.SkipHandler();
                }

                if (wsFederationMessage.Wresult == null)
                {
                    return HandleRequestResult.SkipHandler();
                }

                var token = wsFederationMessage.GetToken();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return HandleRequestResult.SkipHandler();
                }

                var securityTokenContext = await RunSecurityTokenReceivedEventAsync(wsFederationMessage, AuthenticationScheme);

                if (securityTokenContext.Response == null)
                {
                    return HandleRequestResult.SkipHandler();
                }

                ClaimsPrincipal principal = new ClaimsPrincipal();
                ClaimsIdentity ci2 = SefazIdentityTokenValidation.ExtraiClaimsIdentity(token,
                                                                                   _configMachine["SefazIdentity:Wtrealm"],
                                                                                   _configMachine["SefazIdentity:TipoLogin"],
                                                                                   _configMachine["SefazIdentity:TokenWS"]);
                principal.AddIdentity(ci2);

                // Retornar nova instancia ou cacheado
                var state = wsFederationMessage.Wctx;
                
                var properties = GetPropertiesFromWctx(state);
                properties.RedirectUri = BuildWreply(_configMachine["SefazIdentity:Wreply"]);

                var ticket = new AuthenticationTicket(principal, properties,
                    WsFederationAuthenticationDefaults.AuthenticationType);

                if (Options.UseTokenLifetime)
                {
                    ticket.Properties.IssuedUtc = DateTime.Now.ToUniversalTime();
                    ticket.Properties.ExpiresUtc = DateTime.Now.ToUniversalTime();
                    ticket.Properties.AllowRefresh = false;
                }

                var securityTokenValidatedNotification = await RunSecurityTokenValidatedEventAsync(wsFederationMessage,
                    ticket, AuthenticationScheme);

                return HandleRequestResult.Success(ticket);
            }
            catch (Exception exception)
            {
                var authenticationFailedNotification = await RunAuthenticationFailedEventAsync(wsFederationMessage,
                    exception, AuthenticationScheme);

                return HandleRequestResult.Fail(exception);
            }
        }

        protected override Task<HandleRequestResult> HandleAccessDeniedErrorAsync(AuthenticationProperties properties)
        {
            return base.HandleAccessDeniedErrorAsync(properties);
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            return base.HandleForbiddenAsync(properties);
        }

        /// <summary>
        ///     Handles signout
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //protected override async Task HandleSignOutAsync(SignOutContext context)
        //{
        //    if (context == null)
        //    {
        //        return;
        //    }

        //    Logger.LogTrace($"Entering {nameof(WsFederationAuthenticationHandler)}'s HandleSignOutAsync");

        //    if (_configuration == null && Options.ConfigurationManager != null)
        //    {
        //        _configuration = await Options.ConfigurationManager.GetConfigurationAsync(Context.RequestAborted);
        //    }

        //    var wsFederationMessage = new WsFederationMessage
        //    {
        //        IssuerAddress = _configuration.TokenEndpoint ?? string.Empty,
        //        Wtrealm = Options.Wtrealm,
        //        Wreply = Options.Wreply,
        //        Wa = WsFederationActions.SignOut
        //    };

        //    var properties = new AuthenticationProperties(context.Properties);
        //    if (!string.IsNullOrEmpty(properties?.RedirectUri))
        //    {
        //        wsFederationMessage.Wreply = properties.RedirectUri;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(Options.SignOutWreply))
        //    {
        //        wsFederationMessage.Wreply = Options.SignOutWreply;
        //    }
        //    else if (!string.IsNullOrWhiteSpace(Options.Wreply))
        //    {
        //        wsFederationMessage.Wreply = Options.Wreply;
        //    }

        //    var redirectContext = new RedirectContext(Context, Options)
        //    {
        //        ProtocolMessage = wsFederationMessage,
        //    };
        //    await Options.Events.RedirectToIdentityProvider(redirectContext);
        //    if (redirectContext.HandledResponse)
        //    {
        //        Logger.LogDebug("RedirectContext.HandledResponse");
        //        return;
        //    }
        //    if (redirectContext.Skipped)
        //    {
        //        Logger.LogDebug("RedirectContext.Skipped");
        //        return;
        //    }

        //    var redirectUri = redirectContext.ProtocolMessage.CreateSignOutUrl();
        //    if (!Uri.IsWellFormedUriString(redirectUri, UriKind.Absolute))
        //    {
        //        Logger.LogWarning($"The sign-out redirect URI is malformed: {redirectUri}");
        //    }

        //    //System.IO.File.WriteAllText("C:\\Logger\\Log_" + "HandleSignOutAsync_" + DateTime.Now.ToString("HHmmss") + ".txt", redirectUri);

        //    Response.Redirect(redirectUri);
        //}

        private AuthenticationProperties? GetPropertiesFromWctx(string state)
        {
            AuthenticationProperties properties = null;
            if (!string.IsNullOrEmpty(state))
            {
                var pairs = ParseDelimited(state);
                List<string> values;
                if (pairs.TryGetValue(WsFederationAuthenticationDefaults.WctxKey, out values) && values.Count > 0)
                {
                    var value = values.First();
                    properties = _options.CurrentValue.StateDataFormat.Unprotect(value);
                }
            }
            return properties;
        }


        private async Task<MessageReceivedContext> RunMessageReceivedEventAsync(WsFederationMessage message, AuthenticationScheme authenticationScheme)
        {
            var messageReceivedContext = new MessageReceivedContext(Context, authenticationScheme, Options)
            {
                ProtocolMessage = message
            };

            await Options.Events.MessageReceived(messageReceivedContext);
            if (messageReceivedContext.Response == null)
            {
                Logger.LogDebug("MessageReceivedContext.HandledResponse");
            }
            return messageReceivedContext;
        }

        private async Task<SecurityTokenContext> RunSecurityTokenReceivedEventAsync(WsFederationMessage message, AuthenticationScheme authenticationScheme)
        {
            var securityTokenContext = new SecurityTokenContext(Context, authenticationScheme, Options)
            {
                ProtocolMessage = message
            };

            await Options.Events.SecurityTokenReceived(securityTokenContext);
            if (securityTokenContext.Response == null)
            {
                Logger.LogDebug("SecurityTokenContext.HandledResponse");
            }
            return securityTokenContext;
        }

        private async Task<SecurityTokenValidatedContext> RunSecurityTokenValidatedEventAsync(
            WsFederationMessage message,
            AuthenticationTicket ticket,
            AuthenticationScheme authenticationScheme)
        {
            var securityTokenValidateContext = new SecurityTokenValidatedContext(Context, authenticationScheme, Options)
            {
                ProtocolMessage = message,
            };

            await Options.Events.SecurityTokenValidated(securityTokenValidateContext);
            if (securityTokenValidateContext.Response == null)
            {
                Logger.LogDebug("SecurityTokenValidatedContext.HandledResponse");
            }
            return securityTokenValidateContext;
        }

        private async Task<AuthenticationFailedContext> RunAuthenticationFailedEventAsync(WsFederationMessage message,
            Exception exception, AuthenticationScheme authenticationScheme)
        {
            Logger.LogTrace("AuthenticationFailed");
            var authenticationFailedContext = new AuthenticationFailedContext(Context, authenticationScheme, Options)
            {
                ProtocolMessage = message,
                Exception = exception
            };

            await Options.Events.AuthenticationFailed(authenticationFailedContext);
            if (authenticationFailedContext.Response == null)
            {
                Logger.LogDebug("AuthenticationFailedContext.HandledResponse");
            }

            return authenticationFailedContext;
        }

        private string BuildWreply(string targetPath)
        {
            return Request.Scheme + "://" + Request.Host + OriginalPathBase + targetPath;
        }

        private static IDictionary<string, List<string>> ParseDelimited(string text)
        {
            char[] delimiters = { '&', ';' };
            var accumulator = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            var textLength = text.Length;
            var equalIndex = text.IndexOf('=');
            if (equalIndex == -1)
            {
                equalIndex = textLength;
            }
            var scanIndex = 0;
            while (scanIndex < textLength)
            {
                var delimiterIndex = text.IndexOfAny(delimiters, scanIndex);
                if (delimiterIndex == -1)
                {
                    delimiterIndex = textLength;
                }
                if (equalIndex < delimiterIndex)
                {
                    while (scanIndex != equalIndex && char.IsWhiteSpace(text[scanIndex]))
                        ++scanIndex;
                    var name = text.Substring(scanIndex, equalIndex - scanIndex);
                    var value = text.Substring(equalIndex + 1, delimiterIndex - equalIndex - 1);

                    name = Uri.UnescapeDataString(name.Replace('+', ' '));
                    value = Uri.UnescapeDataString(value.Replace('+', ' '));

                    List<string> existing;
                    if (!accumulator.TryGetValue(name, out existing))
                    {
                        accumulator.Add(name, new List<string>(1) { value });
                    }
                    else
                    {
                        existing.Add(value);
                    }

                    equalIndex = text.IndexOf('=', delimiterIndex);
                    if (equalIndex == -1)
                    {
                        equalIndex = textLength;
                    }
                }
                scanIndex = delimiterIndex + 1;
            }
            return accumulator;
        }


    }
}