using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Extensions
{
    public static class ApplicationExtensions
    {
        public static async Task SeedInitialAdminInProdAsync(
            this WebApplication app,
            IConfiguration configuration
        )
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = configuration["AdminUser:Email"];
            var adminUsername = configuration["AdminUser:Username"];
            var adminPassword = configuration["AdminUser:Password"];

            if (
                !string.IsNullOrEmpty(adminEmail)
                && !string.IsNullOrEmpty(adminUsername)
                && !string.IsNullOrEmpty(adminPassword)
            )
            {
                var adminUser = await userManager.FindByNameAsync(adminUsername);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminUsername,
                        Email = adminEmail,
                        EmailConfirmed = true,
                    };
                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                        );
                    }
                }
            }
        }
    }
}
