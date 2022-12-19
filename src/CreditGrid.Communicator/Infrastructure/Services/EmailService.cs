using CreditGrid.Communicator.Domain.Interfaces;
using CreditGrid.Communicator.Infrastructure.SendGrid;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace CreditGrid.Communicator.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient sendGridClient;
        private readonly SenderOption senderOption;

        public EmailService(SendGridClient sendGridClient, IOptions<SenderOption> senderOption)
        {
            this.sendGridClient = sendGridClient;
            this.senderOption = senderOption.Value;
        }

        public async Task<HttpStatusCode> SendEmailAsync(string recipientsName, string recipientsEmail, string? subject, string body)
        {

            var mailMessage = MailHelper.CreateSingleEmail(
                from: new EmailAddress(email: this.senderOption.SendersEmail, name: this.senderOption.SendersName),
                to: new EmailAddress(recipientsEmail, recipientsName),
                subject: subject ?? "No Subject",
                plainTextContent: body,
                htmlContent: body);

            var response = await this.sendGridClient.SendEmailAsync(mailMessage);
            return response.StatusCode;
        }
    }
}
