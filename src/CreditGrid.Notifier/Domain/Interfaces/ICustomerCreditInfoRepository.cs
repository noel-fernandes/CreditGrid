using CreditGrid.Notifier.Infrastructure.Persistence.Models;

namespace CreditGrid.Notifier.Domain.Interfaces
{
    public interface ICustomerCreditInfoRepository : IGenericRepository<CustomerCreditInformation>
    {
        Task<CustomerCreditInformation> GetByCreditNumberAsync(string creditNumber);
    }
}