using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.WsFederation;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation
{
    public class BaseWsFederationContext : BaseContext<WsFederationAuthenticationOptions>
    {
        public BaseWsFederationContext(HttpContext context, AuthenticationScheme scheme, WsFederationAuthenticationOptions options) : base(context, scheme, options)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public WsFederationAuthenticationOptions Options { get; }

        public WsFederationMessage ProtocolMessage { get; set; }
    }
}