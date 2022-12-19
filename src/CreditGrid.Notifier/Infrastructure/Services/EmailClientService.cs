using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Services.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace CreditGrid.Notifier.Infrastructure.Services
{
    public class EmailClientService : IEmailClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly EmailClientServiceOption emailClientServiceOption;

        public EmailClientService(IHttpClientFactory httpClientFactory, IOptions<EmailClientServiceOption> emailClientServiceOption)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.emailClientServiceOption = emailClientServiceOption.Value;
        }

        public async Task<string> SendEmailAsync(string recipientsName, string recipientsEmail, string subject, string messageBody, DateTimeOffset sentOn)
        {
            var emailServerResponse = string.Empty;
            var emailMessage = new EmailMessageDto()
            {
                Recipient = new Recipient
                {
                    Name = recipientsName,
                    Email = recipientsEmail
                },
                Subject = subject,
                MessageBody = messageBody,
                SentOn = sentOn
            };

            var baseAddress = new Uri(this.emailClientServiceOption.BaseAddress);
            var slug = this.emailClientServiceOption.Slug;

            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = baseAddress;
            HttpContent body = new StringContent(JsonConvert.SerializeObject(emailMessage), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(slug, body);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if(!string.IsNullOrWhiteSpace(content)) 
                {
                    emailServerResponse = JsonConvert.DeserializeObject<string>(content);
                }
            }

            return emailServerResponse;
        }
    }
}
