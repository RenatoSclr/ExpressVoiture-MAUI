using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.DataAccess.Data
{
    public class SeedIdentityData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin" };
            IdentityResult roleResult;

            // Vérifier et créer les rôles s'ils n'existent pas
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Créer l'utilisateur Admin
            IdentityUser user = await userManager.FindByEmailAsync("admin@112.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "admin@112.com",
                    Email = "admin@112.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
