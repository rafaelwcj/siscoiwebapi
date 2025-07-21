using Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.AspNetCore.Builder
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddCustomAuthentication(this AuthenticationBuilder builder, Action<WsFederationAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<WsFederationAuthenticationOptions, WsFederationAuthenticationHandler>(WsFederationAuthenticationDefaults.AuthenticationType, configureOptions);
        }
    }
}