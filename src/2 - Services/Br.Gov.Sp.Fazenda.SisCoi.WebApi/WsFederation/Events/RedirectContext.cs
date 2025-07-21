using Microsoft.AspNetCore.Authentication;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation
{
    public class RedirectContext : BaseWsFederationContext
    {
        public RedirectContext(HttpContext context, AuthenticationScheme scheme, WsFederationAuthenticationOptions options) : base(context, scheme, options)
        {
        }

        public AuthenticationProperties Properties { get; set; }
    }
}