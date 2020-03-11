using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using PatientOptOutAPI.Models;

namespace PatientOptOutAPI.Services
{
    public class WindowsClaimsTransformer : IClaimsTransformation
    {
        public static string OptOutClaim = "OptOutChecker";
        private readonly IOptions<ApplicationSettings> _config;

        public WindowsClaimsTransformer(IOptions<ApplicationSettings> config)
        {
            _config = config;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            var claimsIdentity = new ClaimsIdentity(identity.Claims, identity.AuthenticationType, identity.NameClaimType, identity.RoleClaimType);

            var optOutCheckerAccess = principal.CheckAccess(_config.Value.ActiveDirectoryGroupName);
            if (optOutCheckerAccess)
            {
                claimsIdentity.AddClaim(new Claim(OptOutClaim, "True"));
            }

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
