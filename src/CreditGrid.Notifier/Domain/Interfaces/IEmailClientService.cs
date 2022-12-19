namespace CreditGrid.Notifier.Domain.Interfaces
{
    public interface IEmailClientService
    {
        Task<string> SendEmailAsync(string recipientsName, string recipientsEmail, string subject, string messageBody, DateTimeOffset sentOn);
    }
}