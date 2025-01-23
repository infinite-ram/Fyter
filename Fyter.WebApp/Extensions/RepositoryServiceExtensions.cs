using Fyter.Plugins.EFCoreSqlite;
using Fyter.Plugins.Filters;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Extensions
{
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            // Uncommnet below line to use in-memory data
            // services.AddSingleton<IFighterRepository, FighterRepository>();

            services.AddTransient<IFighterRepository, FighterEFCoreRepository>();
            services.AddTransient<IFighterRequestRepository, FighterRequestEFCoreRepository>();

            services.AddScoped<IFighterFilterRepository, FighterFilterRepository>();
            services.AddScoped<IFighterStatusFilterRepository, FighterStatusFilterRepository>();

            return services;
        }
    }
}
