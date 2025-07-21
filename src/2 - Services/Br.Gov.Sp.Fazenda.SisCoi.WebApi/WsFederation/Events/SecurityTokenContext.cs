using Microsoft.AspNetCore.Authentication;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation.Events
{
    public class SecurityTokenContext : BaseWsFederationContext
    {
        public SecurityTokenContext(HttpContext context, AuthenticationScheme scheme, WsFederationAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }
    }
}