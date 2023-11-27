using Order.Data.Entities;

namespace Order.Data.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> InsertOrder(OrderEntity order);

        public IEnumerable<OrderEntity> GetAll();
    }
}
