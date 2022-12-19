namespace CreditGrid.Notifier.Infrastructure.Persistence.Models
{
    public class CustomerCreditInformation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }

        public string CreditNumber { get; set; }

        public decimal AmountDue { get; set; }

        public DateTimeOffset DueDate { get; set; }
    }
}
