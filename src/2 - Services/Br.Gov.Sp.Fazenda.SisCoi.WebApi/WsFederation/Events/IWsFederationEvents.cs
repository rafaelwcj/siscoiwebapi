﻿using Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation.Events;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation
{
    /// <summary>
    /// Specifies events which the <see cref="WsFederationAuthenticationMiddleware"></see> invokes to enable developer control over the authentication process. />
    /// </summary>
    public interface IWsFederationEvents
    {
        /// <summary>
        /// Invoked if exceptions are thrown during request processing. The exceptions will be re-thrown after this event unless suppressed.
        /// </summary>
        Task AuthenticationFailed(AuthenticationFailedContext context);

        /// <summary>
        /// Invoked when a protocol message is first received.
        /// </summary>
        Task MessageReceived(MessageReceivedContext context);

        /// <summary>
        /// Invoked to manipulate redirects to the identity provider for SignIn, SignOut, or Challenge.
        /// </summary>
        Task RedirectToIdentityProvider(RedirectContext context, IConfiguration config);

        /// <summary>
        /// Invoked with the security token that has been extracted from the protocol message.
        /// </summary>
        Task SecurityTokenReceived(SecurityTokenContext context);

        /// <summary>
        /// Invoked after the security token has passed validation and a ClaimsIdentity has been generated.
        /// </summary>
        Task SecurityTokenValidated(SecurityTokenValidatedContext context);
    }
}