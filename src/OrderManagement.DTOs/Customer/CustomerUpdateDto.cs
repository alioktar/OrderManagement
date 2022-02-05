using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class CustomerUpdateDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
