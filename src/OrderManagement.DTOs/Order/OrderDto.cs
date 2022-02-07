using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderDto : IDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public CustomerOrderDto Customer { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
