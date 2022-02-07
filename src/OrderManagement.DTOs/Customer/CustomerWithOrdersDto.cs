using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class CustomerWithOrdersDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<OrderDto> Orders { get; set; }
        public ICollection<AddressDto> Addresses { get; set; }
    }
}
