using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderUpdateDto : IDto
    {
        public int Id { get; set; }
        public AddressUpdateDto ShippingAddress { get; set; }
        public IEnumerable<ProductUpdateDto> Products { get; set; }
    }
}
