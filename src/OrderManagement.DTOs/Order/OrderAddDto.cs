using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderAddDto : IDto
    {
        public CustomerOrderDto Customer { get; set; }
        public AddressAddDto ShippingAddress { get; set; }
        public IEnumerable<OrderProductDto> Products { get; set; }
    }

    public class ExistsCustomerOrderAddDto : IDto
    {
        public int AddressId { get; set; }

        public AddressAddDto ShippingAddress { get; set; }
        public IEnumerable<OrderProductDto> Products { get; set; }
    }
}
