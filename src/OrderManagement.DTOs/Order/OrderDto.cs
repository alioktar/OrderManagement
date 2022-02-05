using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderDto : IDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public CustomerDto Customer { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
