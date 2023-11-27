using Microsoft.Extensions.Logging;
using NSubstitute;
using Order.API.DTOs;
using Order.API.Services;
using Order.Data.Entities;
using Order.Data.Interfaces;

namespace Order.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        private IOrderRepository _orderRepositorySubstitute = Substitute.For<IOrderRepository>();

        public OrderServiceTests() { 
            _orderService = new(Substitute.For<ILogger<OrderService>>(), _orderRepositorySubstitute);
        }


        [Fact]
        public async Task CreateOrderSuccess()
        {
            //Arrange
            CreateOrderDTO orderDTO = GetCreateOrderDTOMock();
            OrderEntity orderEntity = new();

            _orderRepositorySubstitute.InsertOrder(Arg.Do<OrderEntity>(x => orderEntity = x)).Returns(true);

            //Act
            await _orderService.AddOrder(orderDTO);

            //Assert
            Assert.Equal(orderDTO.Client.Name, orderEntity.Client.Name);
            Assert.Equal(orderDTO.Client.Email, orderEntity.Client.Email);
            Assert.Equal(orderDTO.Client.Phone, orderEntity.Client.Phone);

            Assert.Equal(orderDTO.Client.BillingAddress.Addr1, orderEntity.Client.BillingAddress.Addr1);
            Assert.Equal(orderDTO.Client.BillingAddress.Addr2, orderEntity.Client.BillingAddress.Addr2);
            Assert.Equal(orderDTO.Client.BillingAddress.City, orderEntity.Client.BillingAddress.City);
            Assert.Equal(orderDTO.Client.BillingAddress.State, orderEntity.Client.BillingAddress.State);
            Assert.Equal(orderDTO.Client.BillingAddress.Country, orderEntity.Client.BillingAddress.Country);
            Assert.Equal(orderDTO.Client.BillingAddress.PostalCode, orderEntity.Client.BillingAddress.PostalCode);

            Assert.Equal(orderDTO.Client.ShippingAddress.Addr1, orderEntity.Client.ShippingAddress.Addr1);
            Assert.Equal(orderDTO.Client.ShippingAddress.Addr2, orderEntity.Client.ShippingAddress.Addr2);
            Assert.Equal(orderDTO.Client.ShippingAddress.City, orderEntity.Client.ShippingAddress.City);
            Assert.Equal(orderDTO.Client.ShippingAddress.State, orderEntity.Client.ShippingAddress.State);
            Assert.Equal(orderDTO.Client.ShippingAddress.Country, orderEntity.Client.ShippingAddress.Country);
            Assert.Equal(orderDTO.Client.ShippingAddress.PostalCode, orderEntity.Client.ShippingAddress.PostalCode);

            Assert.Equal(orderDTO.Items.Count, orderEntity.Items.Count);
            Assert.Equal(orderDTO.Items.FirstOrDefault().Sku, orderEntity.Items.FirstOrDefault().Sku);
            Assert.Equal(orderDTO.Items.FirstOrDefault().Price, orderEntity.Items.FirstOrDefault().Price);
            Assert.Equal(orderDTO.Items.FirstOrDefault().Quantity, orderEntity.Items.FirstOrDefault().Quantity);
        }

        private CreateOrderDTO GetCreateOrderDTOMock()
        {
            return new CreateOrderDTO()
            {
                Currency = "BRL",
                Client = new()
                {
                    Name = "Test",
                    Email = "test@test.com",
                    Phone = "+5541999999999",
                    BillingAddress = new AddressDTO()
                    {
                        Addr1 = "Rua AAY",
                        Addr2 = "270",
                        City = "CWB",
                        State = "PR",
                        Country = "BR",
                        PostalCode = "88888-888"
                    },
                    ShippingAddress = new AddressDTO()
                    {
                        Addr1 = "Rua FB",
                        Addr2 = "50",
                        City = "CWB",
                        State = "PR",
                        Country = "BR",
                        PostalCode = "77777-777"
                    }
                },
                Items = new() {
                    new()
                    {
                        Sku = "TestSKU0001",
                        Quantity = 1,
                        Price = 10.99f
                    }
                }
            };
        }
    }
}
