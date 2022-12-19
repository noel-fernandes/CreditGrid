namespace CreditGrid.Notifier.Infrastructure.Persistence.Models
{
    public class SentMessage
    {
        public Guid Id { get; set; }

        public string RecipientsName { get; set; }

        public string RecipientsEmail { get; set; }

        public string Subject { get; set; }

        public string BodyContent { get; set; }

        public DateTimeOffset SentOn { get; set; }

        public string ServerResponse { get; set; }
    }
}
