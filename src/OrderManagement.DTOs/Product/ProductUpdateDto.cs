using OrderManagement.Core.DTOs;

namespace OrderManagement.DTOs
{
    public class ProductUpdateDto : IDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
    }
}
