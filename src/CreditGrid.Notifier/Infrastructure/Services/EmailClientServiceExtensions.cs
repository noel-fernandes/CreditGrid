using CreditGrid.Notifier.Domain.Interfaces;
using CreditGrid.Notifier.Infrastructure.Services.Models;

namespace CreditGrid.Notifier.Infrastructure.Services
{
    public static class EmailClientServiceExtensions
    {
        public static IServiceCollection AddEmailClientService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<EmailClientServiceOption>(configuration.GetSection("EmailService"));
            services.AddTransient<IEmailClientService, EmailClientService>();

            return services;
        }
    }
}
