using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Order : Entity
    {
        public virtual Customer Customer { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
