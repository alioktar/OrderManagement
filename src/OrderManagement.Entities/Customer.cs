using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Customer:Entity
    {
        public string FullName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
