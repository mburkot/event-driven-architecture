namespace Order.API.DTOs
{
    public class CreateOrderDTO
    {
        public string OrderNumber { get; set; }

        public string Currency { get; set; }

        public ClientDTO Client { get; set; }

        public List<ItemDto> Items { get; set; }
    }
}
