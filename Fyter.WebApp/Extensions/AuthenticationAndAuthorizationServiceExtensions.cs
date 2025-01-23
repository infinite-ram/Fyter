using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Components.Account;
using Fyter.WebApp.Data;
using Fyter.WebApp.Services;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Extensions
{
    public static class AuthenticationAndAuthorizationServiceExtensions
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Identity
            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<
                AuthenticationStateProvider,
                IdentityRevalidatingAuthenticationStateProvider
            >();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddIdentityCore<ApplicationUser>(options =>
                    options.SignIn.RequireConfirmedAccount = true
                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender<ApplicationUser>, IdentityEmailSender>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<
                IAuthorizationMiddlewareResultHandler,
                AuthorizationMiddlewareResultHandler
            >();

            return services;
        }
    }
}
