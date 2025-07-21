using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Domain.Core.Claims
{
    public class ClaimsTransformationService : IClaimsTransformation
    {
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated != true)
            {
                return principal;
            }
            var role = principal.FindFirst(ClaimTypes.Role).Value;
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
            return await Task.FromResult(principal);
        }
    }
}
