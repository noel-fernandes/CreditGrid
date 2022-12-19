using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Models
{
    public class SentMessageEntityTypeConfiguration : IEntityTypeConfiguration<SentMessage>
    {
        public void Configure(EntityTypeBuilder<SentMessage> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedNever();

            builder.Property(k => k.RecipientsName).IsRequired();
            builder.Property(k => k.RecipientsEmail).IsRequired();
            builder.Property(k => k.Subject).IsRequired();
            builder.Property(k => k.BodyContent).IsRequired();
            builder.Property(k => k.SentOn).IsRequired();
            builder.Property(k => k.ServerResponse).IsRequired();
        }
    }
}
