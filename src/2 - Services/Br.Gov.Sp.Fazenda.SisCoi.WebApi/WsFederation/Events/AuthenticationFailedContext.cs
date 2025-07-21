using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Br.Gov.Sp.Fazenda.SisCoi.WebApi.WsFederation
{
    public class AuthenticationFailedContext : BaseWsFederationContext
    {
        public AuthenticationFailedContext(HttpContext context, AuthenticationScheme scheme, WsFederationAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public Exception Exception { get; set; }
    }
}