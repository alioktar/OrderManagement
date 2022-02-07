using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class OrderProductDto : IDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
