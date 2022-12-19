using CreditGrid.Notifier.Domain.Interfaces;
using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CreditGridNotifierContext context;

        public GenericRepository(CreditGridNotifierContext context)
        {
            this.context = context;
        }

        public async Task AddEntityAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context.Dispose();
            }
        }
    }
}
