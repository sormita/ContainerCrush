using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginFunctionality.Models.Identity
{
    public class AppUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<User>
    {
        public AppUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            IOptions<IdentityOptions> options
            ): base (userManager,options)
        {

        }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            new Claim(ClaimTypes.GivenName, user.PersonName)
        });

            return principal;
        }

        //protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        //{
        //    var identity= await base.GenerateClaimsAsync(user);
        //    identity.AddClaim(new Claim("FullName", user.Name));

        //    return identity;
        //}
    }
}
