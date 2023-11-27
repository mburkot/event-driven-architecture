using Order.Broker.Brokers;
using Order.Broker.Interfaces;

namespace Order.API.Extensions
{
    public static class BrokerExtension
    {
        public static IServiceCollection RegisterBroker(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IBroker, MongoDBOrderRepository>(sp => new MongoDBOrderRepository(configuration.GetConnectionString("MongoDB"), sp.GetRequiredService<ILogger<MongoDBOrderRepository>>()));
            services.AddSingleton<IBroker, InMemoryBroker>();
            return services;
        }
    }
}
