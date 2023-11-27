using Order.API.DTOs;
using Order.Data.Entities;
using Order.Data.Interfaces;

namespace Order.API.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddOrder(CreateOrderDTO dto)
        {
            return await _orderRepository.InsertOrder(ConvertToOrderEntity(dto));
        }

        public IEnumerable<OrderEntity> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        private OrderEntity ConvertToOrderEntity(CreateOrderDTO dto)
        {
            var entity = new OrderEntity
            {
                Currency = dto.Currency,
                Client = new()
                {
                    Name = dto.Client.Name,
                    Email = dto.Client.Email,
                    Phone = dto.Client.Phone,
                    BillingAddress = new()
                    {
                        Addr1 = dto.Client.BillingAddress.Addr1,
                        Addr2 = dto.Client.BillingAddress.Addr2,
                        City = dto.Client.BillingAddress.City,
                        State = dto.Client.BillingAddress.State,
                        Country = dto.Client.BillingAddress.Country,
                        PostalCode = dto.Client.BillingAddress.PostalCode
                    },
                    ShippingAddress = new()
                    {
                        Addr1 = dto.Client.ShippingAddress.Addr1,
                        Addr2 = dto.Client.ShippingAddress.Addr2,
                        City = dto.Client.ShippingAddress.City,
                        State = dto.Client.ShippingAddress.State,
                        Country = dto.Client.ShippingAddress.Country,
                        PostalCode = dto.Client.ShippingAddress.PostalCode
                    }
                },
                Items = new()
            };

            foreach (var item in dto.Items)
            {
                entity.Items.Add(new()
                {
                    Sku = item.Sku,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            entity.TotalPrice = entity.Items.Sum(x => x.Price * x.Quantity);

            return entity;
        }
    }
}
