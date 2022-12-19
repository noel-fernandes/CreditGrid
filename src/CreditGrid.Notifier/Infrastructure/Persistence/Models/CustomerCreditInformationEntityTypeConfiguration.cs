using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Models
{
    public class CustomerCreditInformationEntityTypeConfiguration : IEntityTypeConfiguration<CustomerCreditInformation>
    {
        public void Configure(EntityTypeBuilder<CustomerCreditInformation> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedNever();

            builder.Property(k => k.Email).IsRequired();

            builder.Property(k => k.CreditNumber).IsRequired();

            builder.Property(k => k.AmountDue).IsRequired();

            builder.Property(k => k.DueDate).IsRequired();
        }
    }
}
