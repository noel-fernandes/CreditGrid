using CreditGrid.Notifier.Domain.Interfaces;

namespace CreditGrid.Notifier.Infrastructure.Persistence.Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services) 
        {
            services.AddTransient<ITemplatesRepository, TemplatesRepository>();
            services.AddTransient<ICustomerCreditInfoRepository, CustomerCreditInfoRepository>();
            services.AddTransient<ISentMessagesRepository, SentMessagesRepository>();
            return services;
        }
    }
}
