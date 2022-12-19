using CreditGrid.Notifier.Domain.Enums;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;

namespace CreditGrid.Notifier.Domain.Interfaces
{
    public interface ITemplatesRepository : IGenericRepository<Template>
    {

        Task<Template> GetByTypeAsync(TemplateType templateType);
    }
}