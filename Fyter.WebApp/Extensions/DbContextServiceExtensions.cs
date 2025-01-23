using Microsoft.EntityFrameworkCore;
using Fyter.Plugins.EFCoreSqlite;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Extensions
{
    public static class DbContextServiceExtensions
    {
        public static IServiceCollection AddDatabaseContexts(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var identityDBConnectionString =
                configuration.GetConnectionString("IdentityDB")
                ?? throw new InvalidOperationException("Connection string 'IdentityDB' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(identityDBConnectionString)
            );

            services.AddDbContextFactory<FyterSqliteContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("FyterDB"));
            });

            return services;
        }
    }
}
