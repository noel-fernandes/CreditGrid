namespace CreditGrid.Notifier.Infrastructure.Services.Models
{
    public class EmailMessageDto
    {
        public Recipient Recipient { get; init; }

        public string? Subject { get; init; }

        public string MessageBody { get; init; }

        public DateTimeOffset SentOn { get; init; }
    }

    public class Recipient
    {
        public string Name { get; init; }

        public string Email { get; init; }
    }
}
