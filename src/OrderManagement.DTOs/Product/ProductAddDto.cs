using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class ProductAddDto : IDto
    {
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
    }
}
