using Microsoft.AspNetCore.Authentication;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation.Events
{
    public class SecurityTokenValidatedContext : BaseWsFederationContext
    {
        public SecurityTokenValidatedContext(HttpContext context, AuthenticationScheme scheme, WsFederationAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }
    }
}