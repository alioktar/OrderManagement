using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Customer:Entity
    {
        public string FullName { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
