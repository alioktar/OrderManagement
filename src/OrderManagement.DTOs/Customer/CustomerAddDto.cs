using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class CustomerAddDto : IDto
    {
        public string FullName { get; set; }

        public ICollection<AddressAddDto> Addresses { get; set; }
    }
}
