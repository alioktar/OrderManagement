using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderUpdateDto : IDto
    {
        public AddressAddDto ShippingAddress { get; set; }
        public IEnumerable<OrderProductDto> Products { get; set; }
    }
}
