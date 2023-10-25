using Order.Data.Entities;
using Order.Data.Interfaces;

namespace Order.Data.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<OrderEntity> _orders;

        public InMemoryOrderRepository() {
            _orders = new();
        }

        public Task<bool> InsertOrder(OrderEntity order)
        {
            _orders.Add(order);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<OrderEntity>> GetAll()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }
    }
}
