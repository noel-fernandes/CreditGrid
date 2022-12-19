using CreditGrid.Notifier.Domain.Enums;
using CreditGrid.Notifier.Domain.Exceptions;
using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Repositories
{
    public class TemplatesRepository : GenericRepository<Template>, ITemplatesRepository
    {
        public TemplatesRepository(CreditGridNotifierContext context) : base(context)
        {
        }

        public async Task<Template> GetByTypeAsync(TemplateType templateType)
        {
            var template = await this.context.Templates.FirstOrDefaultAsync(t => t.TemplateType == templateType);

            if (template == null)
            {
                throw new TemplateNotFoundException();
            }

            return template;
        }
    }
}
