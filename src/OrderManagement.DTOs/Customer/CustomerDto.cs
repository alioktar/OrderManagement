using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class CustomerDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<AddressDto> Addresses { get; set; }
    }
}
