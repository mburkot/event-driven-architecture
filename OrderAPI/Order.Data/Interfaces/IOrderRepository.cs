using Order.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Data.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> InsertOrder(OrderEntity order);

        public Task<IEnumerable<OrderEntity>> GetAll();
    }
}
