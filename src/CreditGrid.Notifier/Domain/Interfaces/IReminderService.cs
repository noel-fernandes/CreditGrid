using CreditGrid.Notifier.Infrastructure.Persistence.Models;

namespace CreditGrid.Notifier.Domain.Interfaces
{
    public interface IReminderService
    {
        Task<string> RaiseReminderAsync(string creditNumber);

    }
}