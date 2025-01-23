using Microsoft.Data.Sqlite;

namespace Fyter.WebApp.Services
{
    public class DatabaseBackupService
    {
        private readonly string _identityDbConnectionString;
        private readonly string _FyterDbConnectionString;
        private readonly string _backupDirectory;

        public DatabaseBackupService(IConfiguration configuration)
        {
            _identityDbConnectionString = configuration.GetConnectionString("IdentityDB");
            _FyterDbConnectionString = configuration.GetConnectionString("FyterDB");
            _backupDirectory = Path.Combine(AppContext.BaseDirectory, "Data", "Backups");

            // Ensure the backup directory exists
            Directory.CreateDirectory(_backupDirectory);
        }

        public async Task BackupDatabasesAsync()
        {
            await BackupDatabaseAsync(
                _identityDbConnectionString,
                Path.Combine(_backupDirectory, "identitydb_backup.db")
            );
            await BackupDatabaseAsync(
                _FyterDbConnectionString,
                Path.Combine(_backupDirectory, "FyterDB_backup.db")
            );
        }

        private async Task BackupDatabaseAsync(string sourceConnectionString, string backupFilePath)
        {
            try
            {
                using var sourceConnection = new SqliteConnection(sourceConnectionString);
                await sourceConnection.OpenAsync();

                var backupConnectionString = $"Data Source={backupFilePath}";
                using var backupConnection = new SqliteConnection(backupConnectionString);
                await backupConnection.OpenAsync();

                sourceConnection.BackupDatabase(backupConnection);

                Console.WriteLine($"Database backup completed: {backupFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error backing up database to {backupFilePath}: {ex.Message}");
            }
        }
    }
}
