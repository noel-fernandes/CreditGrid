using CreditGrid.Notifier.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditGrid.Notifier.Infrastructure.Persistence
{
    public class CreditGridNotifierContext : DbContext
    {
        public CreditGridNotifierContext(DbContextOptions<CreditGridNotifierContext> options) : base(options) { }

        public DbSet<Template> Templates { get; set; }

        public DbSet<CustomerCreditInformation> CustomerCreditInformation { get; set; }

        public DbSet<SentMessage> SentMessages { get; set; }
    }
}
