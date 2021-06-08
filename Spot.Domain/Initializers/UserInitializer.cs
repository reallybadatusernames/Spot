using Microsoft.AspNetCore.Identity;
using Spot.Domain.Entities;

namespace Spot.Domain.Initializers
{
    public static class UserInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@Spot.com").Result == null)
            {
                var user = new ApplicationUser { Email = "admin@Spot.com", FirstName = "admin", LastName = "admin" };
                IdentityResult result = userManager.CreateAsync(user, "$p0tDis").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
