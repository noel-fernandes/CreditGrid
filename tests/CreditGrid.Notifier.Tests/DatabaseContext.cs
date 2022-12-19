using CreditGrid.Notifier.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CreditGrid.Notifier.Tests
{
    public static class DatabaseContext
    {

        public static CreditGridNotifierContext Create(bool clearDatabase = true)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CreditGridNotifierContext>();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            optionsBuilder.UseSqlite(connection);
            var context = new CreditGridNotifierContext(optionsBuilder.Options);
            if (clearDatabase)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }
    }
}
