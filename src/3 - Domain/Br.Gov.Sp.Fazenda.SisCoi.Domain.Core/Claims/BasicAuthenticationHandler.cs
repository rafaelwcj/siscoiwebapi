using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.IdentityServer
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var opt = Options;

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unathorization");
            }

            AuthenticationTicket ticket = null;
            string authorizationHeader = Request.Headers["Authorization"];

            if (authorizationHeader != null)
            {
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.Contains("Bearer"))
                {
                    return null;
                }
                else
                {
                    var tokenStringBearer = authorizationHeader.Split("Bearer ")[1];

                    if (tokenStringBearer == "undefined" || string.IsNullOrEmpty(tokenStringBearer))
                    {
                        return null;
                    }

                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken token = jwtSecurityTokenHandler.ReadJwtToken(tokenStringBearer);
                    var listClaims = new List<Claim>();

                    var nameid = (token.Payload["nameid"] != null ? token.Payload["nameid"].ToString() : null);

                    if (nameid != null)
                    {
                        listClaims.Add(new Claim(ClaimTypes.NameIdentifier, nameid));

                        if (token.Payload["role"] != null)
                        {
                            var pay_role = token.Payload["role"].ToString().Split(",".ToCharArray());

                            if (pay_role.Length == 1)
                            {
                                listClaims.Add(new Claim(ClaimTypes.Role, pay_role[0]));
                            }
                            else
                            {
                                var roles_array = (token.Payload["role"] != null ? System.Text.Json.JsonSerializer.Deserialize<string[]>(token.Payload["role"].ToString()) : null);

                                foreach (var roule in roles_array)
                                {
                                    listClaims.Add(new Claim(ClaimTypes.Role, roule));
                                }
                            }
                        }

                        var identity = new ClaimsIdentity(listClaims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        ticket = new AuthenticationTicket(principal, Scheme.Name);

                        return Task.FromResult(AuthenticateResult.Success(ticket)).Result;
                    }
                    else
                    {
                        return Task.FromResult(AuthenticateResult.Fail("Unathorization")).Result;
                    }
                }
            }
            return Task.FromResult(AuthenticateResult.Fail("Unathorization")).Result;
        }
    }
}
