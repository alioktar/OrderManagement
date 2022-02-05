using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class AddressAddDto : IDto
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Description { get; set; }
    }
}
