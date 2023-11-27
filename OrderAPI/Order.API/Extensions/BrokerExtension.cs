using Order.Broker.Brokers;
using Order.Broker.Interfaces;

namespace Order.API.Extensions
{
    public static class BrokerExtension
    {
        public static IServiceCollection RegisterBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBroker, ServiceBusBroker>(sp => new ServiceBusBroker(configuration.GetConnectionString("ServiceBus"), sp.GetRequiredService<ILogger<ServiceBusBroker>>()));
            return services;
        }
    }
}
