using Fyter.WebApp.Data;
using Fyter.WebApp.Services;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Extensions;

public static class ApplicationCoreServiceExtensions
{
    public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AppMappingProfile).Assembly);
        services.AddAutoMapper(typeof(FighterDtoMappingProfile).Assembly);

        services.AddScoped<LoadingService>();

        services.AddScoped<IFighterPropertyPathService, FighterPropertyPathService>();

        services.AddScoped<IAddFighterFormService, AddFighterFormService>();
        services.AddScoped<IFighterService, FighterService>();
        services.AddScoped<IFighterRequestService, FighterRequestService>();
        services.AddScoped<IFighterValidationService, FighterValidationService>();
        services.AddScoped<IFormDataCacheService, FormDataCacheService>();

        return services;
    }
}
