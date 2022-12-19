using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Repositories
{

    public class SentMessagesRepository : GenericRepository<SentMessage>, ISentMessagesRepository
    {
        private readonly IEmailClientService emailClientService;

        public SentMessagesRepository(CreditGridNotifierContext context, IEmailClientService emailClientService) : base(context)
        {
            this.emailClientService = emailClientService;
        }

        public async Task SendAndAddMessageAsync(string recipientsName, string recipientsEmail, string subject, string messageBody, DateTimeOffset sentOn)
        {
            var serverResponse = await this.emailClientService.SendEmailAsync(recipientsName, recipientsEmail, subject, messageBody, sentOn);

            var sentMessage = new SentMessage()
            {
                Id = Guid.NewGuid(),
                RecipientsName = recipientsName,
                RecipientsEmail = recipientsEmail,
                Subject = subject,
                BodyContent = messageBody,
                ServerResponse = serverResponse
            };
            await this.context.SentMessages.AddAsync(sentMessage);
        }
    }
}
