using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class ProductDto : IDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
    }
}
