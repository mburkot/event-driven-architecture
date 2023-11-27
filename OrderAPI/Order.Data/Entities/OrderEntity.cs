namespace Order.Data.Entities
{
    public class OrderEntity
    {
        public string OrderNumber { get; set; }

        public string Currency { get; set; }

        public float TotalPrice { get; set; }

        public Client Client { get; set; }

        public List<Item> Items { get; set; }
    }

    public class Client
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Address BillingAddress { get; set; }

        public Address ShippingAddress { get; set; }

    }

    public class Address
    {
        public string Addr1 { get; set; }

        public string Addr2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }

    public class Item
    {
        public string Sku { get; set; }

        public float Quantity { get; set; }

        public float Price { get; set; }
    }
}
