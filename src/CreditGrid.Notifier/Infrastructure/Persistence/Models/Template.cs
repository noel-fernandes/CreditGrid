using CreditGrid.Notifier.Domain.Enums;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Models
{
    public class Template
    {
        public Guid Id { get; set; }

        public TemplateType TemplateType { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
