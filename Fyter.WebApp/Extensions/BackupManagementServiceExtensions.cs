using Fyter.WebApp.Services;

namespace Fyter.WebApp.Extensions
{
    public static class BackupManagementServiceExtensions
    {
        public static IServiceCollection AddBackupManagementServices(
            this IServiceCollection services
        )
        {
            services.AddSingleton<DatabaseBackupService>();
            services.AddHostedService<DatabaseBackupBackgroundService>();

            return services;
        }
    }
}
