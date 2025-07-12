using Microsoft.EntityFrameworkCore;

namespace YurtApps.Infrastructure.Helper
{
    public static class MigrationHelper
    {
        public static async Task MigrationControl(DbContext context)
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}
