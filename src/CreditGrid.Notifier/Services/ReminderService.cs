using CreditGrid.Notifier.Domain.Enums;
using CreditGrid.Notifier.Domain.Exceptions;
using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;
using HandlebarsDotNet;

namespace CreditGrid.Notifier.Services
{
    public class ReminderService : IReminderService
    {
        private readonly ICustomerCreditInfoRepository customerCreditInfoRepository;
        private readonly ITemplatesRepository templatesRepository;
        private readonly ISentMessagesRepository sentMessagesRepository;

        public ReminderService(ICustomerCreditInfoRepository customerCreditInfoRepository, ITemplatesRepository templatesRepository,
            ISentMessagesRepository sentMessagesRepository, IEmailClientService emailClientService)
        {
            this.customerCreditInfoRepository = customerCreditInfoRepository;
            this.templatesRepository = templatesRepository;
            this.sentMessagesRepository = sentMessagesRepository;
        }

        public async Task<string> RaiseReminderAsync(string creditNumber)
        {
            var customerCreditInformation = await this.customerCreditInfoRepository.GetByCreditNumberAsync(creditNumber);
            if (customerCreditInformation == null)
            {
                throw new CustomerCreditInformationNotFoundException();
            }


            Template template = null;
            switch (customerCreditInformation.DueDate)
            {
                case DateTimeOffset dueDate when dueDate > DateTimeOffset.UtcNow:
                    template = await this.templatesRepository.GetByTypeAsync(TemplateType.Reminder);
                    break;

                case DateTimeOffset dueDate when dueDate == DateTimeOffset.UtcNow.AddDays(-1):
                    template = await this.templatesRepository.GetByTypeAsync(TemplateType.OverdueReminder);
                    break;

                case DateTimeOffset dueDate when dueDate < DateTimeOffset.UtcNow.AddDays(-1):
                    template = await this.templatesRepository.GetByTypeAsync(TemplateType.Cancellation);
                    break;
            }

            if (template == null)
                return "Could not sent reminder";


            var merged = PerformMerge(template, customerCreditInformation);

            var subject = merged.Item1;
            var messageBody = merged.Item2;
            var sentOn = DateTimeOffset.UtcNow;

            await this.sentMessagesRepository.SendAndAddMessageAsync(customerCreditInformation.Name, customerCreditInformation.Email, 
                subject, messageBody, sentOn);
            await this.sentMessagesRepository.CompleteAsync();

            return "Reminder sent";
        }

        private (string, string) PerformMerge(Template template, CustomerCreditInformation customerCreditInformation)
        {
            var compiledSubjectTemplate = Handlebars.Compile(template.Subject);
            var compiledContentTemplate = Handlebars.Compile(template.Content);

            var subject = compiledSubjectTemplate(customerCreditInformation);
            var messageBody = compiledContentTemplate(customerCreditInformation);

            return (subject, messageBody);
        }
    }
}
