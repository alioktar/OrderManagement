using OrderManagement.Core.Entities;

namespace OrderManagement.Entities
{
    public class Address : Entity
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Description { get; set; }
    }
}
