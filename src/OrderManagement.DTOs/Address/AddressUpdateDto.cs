using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class AddressUpdateDto : IDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Description { get; set; }
    }
}
