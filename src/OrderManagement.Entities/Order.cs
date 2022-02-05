using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Order : Entity
    {
        public string OrderNumber { get; set; }
        public bool IsCancelled { get; set; } = false;

        public virtual Customer Customer { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
