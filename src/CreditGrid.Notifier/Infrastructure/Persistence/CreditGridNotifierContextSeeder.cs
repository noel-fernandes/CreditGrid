using Bogus;
using CreditGrid.Notifier.Domain.Enums;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;

namespace CreditGrid.Notifier.Infrastructure.Persistence
{
    public static class CreditGridNotifierContextSeeder
    {
        public static async Task SeedTemplatesAsync(this CreditGridNotifierContext context)
        {
            var templates = Templates.GenerateTemplates().ToList();
            await context.Templates.AddRangeAsync(templates);

            await context.SaveChangesAsync();
        }

        public static async Task SeedCustomerCreditInformation(this CreditGridNotifierContext context)
        {
            var ccInfo = CustomerCreditInfo.GenerateCustomerCreditInformation();
            await context.CustomerCreditInformation.AddRangeAsync(ccInfo);

            await context.SaveChangesAsync();
        }
    }

    internal static class Templates
    {
        public static IEnumerable<Template> GenerateTemplates()
        {
            var dueReminder = new Template()
            {
                Id = Guid.NewGuid(),
                TemplateType = TemplateType.Reminder,
                Subject = "Payment due reminder for your credit card: {{CreditNumber}}",
                Content = "<p>Dear {{Name}},</p><p>This is to remind you that the amount of EUR {{AmountDue}} on your credit card {{CreditNumber}} is due by {{DueDate}}. Please ensure to make the payment on time.</p><p>Greetings CashGrid team</p>"
            };

            var overdueReminder = new Template()
            {
                Id = Guid.NewGuid(),
                TemplateType = TemplateType.OverdueReminder,
                Subject = "Payment overdue reminder for your credit card: {{CreditNumber}}",
                Content = "<p>Dear {{Name}},</p><p>You have missed to make a payment of EUR {{AmountDue}} on your credit card {{CreditNumber}}. Please make the payment at the earliest to avoid additional charges.</p><p>Greetings CashGrid team</p>"
            };

            var cancellationNotice = new Template()
            {
                Id = Guid.NewGuid(),
                TemplateType = TemplateType.Cancellation,
                Subject = "Cancellation Notice for your credit card: {{CreditNumber}}",
                Content = "<p>Dear {{Name}},</p><p>Despite several reminders, you have not been able to make your payments on time. As a result you have accrued EUR {{AmountDue}} as additional charges on your credit card {{CreditCardNumber}}.</p><p>As a result of this, we have to cancel your credit card. Kindly visit the branch to make the necessary payments. Please note that failure to make the payments may result in a legal proceedings.</p><p>Greetings CashGrid team</p>"
            };

            var templates = new List<Template>()
            {
                dueReminder, overdueReminder, cancellationNotice
            };

            return templates;
        }
    }

    internal static class CustomerCreditInfo
    {
        public static List<CustomerCreditInformation> GenerateCustomerCreditInformation(int count = 30)
        {
            var cciRules = new Faker<CustomerCreditInformation>()
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Name, f => f.Person.FullName)
                .RuleFor(r => r.Phone, f => f.Person.Phone)
                .RuleFor(r => r.Email, f => "mixic37008@randrai.com")
                .RuleFor(r => r.CreditNumber, f => f.Finance.Account())
                .RuleFor(r => r.AmountDue, f => Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(r => r.DueDate, f => f.Date.BetweenOffset(DateTimeOffset.UtcNow.AddDays(-10), DateTimeOffset.UtcNow.AddDays(20)));

            var data = cciRules.Generate(count);
            return data;
        }
    }
}
