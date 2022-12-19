using SendGrid;

namespace CreditGrid.Communicator.Infrastructure.SendGrid
{
    public static class SendGridExtensions
    {
        public static IServiceCollection AddSendGridClient(this IServiceCollection services, IConfiguration configuration)
        {
            var apiKey = configuration["SendGrid:ApiKey"];

            var emailClient = new SendGridClient(apiKey);

            services.AddSingleton(typeof(SendGridClient), emailClient);

            services.Configure<SenderOption>(configuration.GetSection("SendGrid:Sender"));

            return services;
        }
    }
}
