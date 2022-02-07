using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class OrderProduct : Entity
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
