using System.Net;

namespace CreditGrid.Communicator.Domain.Interfaces
{
    public interface IEmailService
    {
        Task<HttpStatusCode> SendEmailAsync(string recipientsName, string recipientsEmail, string? subject, string body);
    }
}
