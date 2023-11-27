using Microsoft.Extensions.DependencyInjection;
using Order.Data.Interfaces;
using Order.Data.Repositories;

namespace Order.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IOrderRepository, MongoDBOrderRepository>(sp => new MongoDBOrderRepository(configuration.GetConnectionString("MongoDB"), sp.GetRequiredService<ILogger<MongoDBOrderRepository>>()));
            return services;
        }

    }
}
