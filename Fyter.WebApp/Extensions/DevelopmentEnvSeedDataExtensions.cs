using Fyter.WebApp.Data;

namespace Fyter.WebApp.Extensions;

public static class DevelopmentEnvSeedDataExtensions
{
    public static async Task SeedDevelopmentEnvironmentDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var configuration = services.GetRequiredService<IConfiguration>();
            var environment = app.Environment;

            if (configuration.GetValue<bool>("SeedData"))
                await DevelopmentDataInitialization.SeedDataAsync(services, environment);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding data in development: {ex.Message}");
        }
    }
}
