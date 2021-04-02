using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace stocktaking2.Blazor.Helpers
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string password = "password";
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (userManager.Users.Count() < 1)
            {
                IdentityUser admin = new IdentityUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
