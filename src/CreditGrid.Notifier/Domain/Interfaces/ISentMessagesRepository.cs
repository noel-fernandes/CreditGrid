using CreditGrid.Notifier.Infrastructure.Persistence.Models;

namespace CreditGrid.Notifier.Domain.Interfaces
{
    public interface ISentMessagesRepository : IGenericRepository<SentMessage> 
    {
        Task SendAndAddMessageAsync(string recipientsName, string recipientsEmail, string subject, string messageBody, DateTimeOffset sentOn);
    }
}
