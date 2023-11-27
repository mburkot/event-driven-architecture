using System.Net;

namespace Order.API.DTOs
{
    public class ClientDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public AddressDTO BillingAddress { get; set; }

        public AddressDTO ShippingAddress { get; set; }
    }
}
