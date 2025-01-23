namespace Fyter.WebApp.Services
{
    public class DatabaseBackupBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseBackupBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Wait for the application to fully start
            await Task.Delay(TimeSpan.FromSeconds(100), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var backupService =
                    scope.ServiceProvider.GetRequiredService<DatabaseBackupService>();

                try
                {
                    await backupService.BackupDatabasesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during database backup: {ex.Message}");
                }

                // Wait for the next backup interval (e.g., every 24 hours)
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
