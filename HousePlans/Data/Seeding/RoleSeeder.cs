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
        private readonly AdminConfiguration adminConfiguration;

        public RolesSeeder(AdminConfiguration adminConfiguration)
        {
            this.adminConfiguration = adminConfiguration;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await SeedRoleAsync(dbContext, roleManager, userManager,this.adminConfiguration ,AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(
            ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            AdminConfiguration adminConfiguration,
            string roleName)
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
                    Email = adminConfiguration.Email,
                    UserName = adminConfiguration.UserName,
                };

                dbContext.Users.Add(user);

                var adminPassword = adminConfiguration.Password;

                var userResult = await userManager.CreateAsync(user, adminPassword);
               
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, AdministratorRoleName);
                }
            }
        }
    }
}
