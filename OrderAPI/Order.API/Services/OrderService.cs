using Order.API.DTOs;
using Order.Data.Entities;
using Order.Data.Interfaces;
using Order.Broker.Interfaces;
using Order.API.Events;
using Order.Broker.Models;

namespace Order.API.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBroker _broker;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger, IOrderRepository orderRepository, IBroker broker)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _broker = broker;
        }

        public async Task<bool> AddOrder(CreateOrderDTO dto)
        {
            var response = await _orderRepository.InsertOrder(ConvertToOrderEntity(dto));

            if(!response)
                return false;

            await PublishNewOrderEvent(dto);

            return true;
        }

        public IEnumerable<OrderEntity> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        private async Task<bool> PublishNewOrderEvent(CreateOrderDTO dto)
        {
            NewOrderEvent newOrderEvent = new()
            {
                OrderNumber = dto.OrderNumber
            };

            EventProperties eventProperties = new()
            {
                Type = "order.new",
                Source = "order.api.create.endpoint",
                Subject = "order.new",
                Id = Guid.NewGuid().ToString(),
            };

            return await _broker.Publish(newOrderEvent, eventProperties);
        }

        private OrderEntity ConvertToOrderEntity(CreateOrderDTO dto)
        {
            var entity = new OrderEntity
            {
                OrderNumber = dto.OrderNumber,
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
