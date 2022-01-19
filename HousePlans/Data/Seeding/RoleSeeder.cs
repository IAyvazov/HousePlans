namespace HousePlans.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static GlobalConstant;

    internal class RolesSeeder : ISeeder
    {

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await SeedRoleAsync(dbContext, roleManager, userManager, AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var newRole = new IdentityRole(roleName);
                var roleResult = await roleManager.CreateAsync(newRole);
                if (!roleResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, roleResult.Errors.Select(e => e.Description)));
                }


                var user = new IdentityUser
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                };

                dbContext.Users.Add(user);

                var adminPassword = "Admin123/";

                var userResult = await userManager.CreateAsync(user, adminPassword);
               
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AdministratorRoleName);
                }
            }
        }
    }
}
