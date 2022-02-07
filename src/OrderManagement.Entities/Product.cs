using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Product : Entity
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
