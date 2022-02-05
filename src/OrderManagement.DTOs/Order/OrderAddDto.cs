using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderAddDto : IDto
    {
        public int AddressId { get; set; }
        public AddressAddDto ShippingAddress { get; set; }
        public IEnumerable<ProductAddDto> Products { get; set; }
    }
}
