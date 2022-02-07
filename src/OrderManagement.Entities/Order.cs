using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Order : Entity
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string OrderNumber { get; set; }
        public bool IsCancelled { get; set; } = false;

        public virtual Address ShippingAddress { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    }
}
