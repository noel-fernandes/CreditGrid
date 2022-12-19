using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Models
{
    public class TemplateEntityTypeConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedNever();
            builder.Property(f => f.TemplateType).IsRequired();
            builder.Property(f => f.Content).IsRequired();
        }
    }
}
