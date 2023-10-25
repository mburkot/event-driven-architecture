using Order.Data.Interfaces;
using Order.Data.Repositories;

namespace Order.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
            return services;
        }

    }
}
