using CreditGrid.Notifier.Domain.Exceptions;
using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Repositories
{
    public class CustomerCreditInfoRepository : GenericRepository<CustomerCreditInformation>, ICustomerCreditInfoRepository
    {

        public CustomerCreditInfoRepository(CreditGridNotifierContext context) : base(context)
        {
        }

        public async Task<CustomerCreditInformation> GetByCreditNumberAsync(string creditNumber)
        {
            var customerInfo = await this.context.CustomerCreditInformation.FirstOrDefaultAsync(c => c.CreditNumber == creditNumber);
            if (customerInfo == null)
            {
                throw new CustomerCreditInformationNotFoundException();
            }
            return customerInfo;
        }
    }
}
