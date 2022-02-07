using OrderManagement.Core.Repository;
using OrderManagement.Entities;

namespace OrderManagement.Repository.Base
{
    public interface IProductRepository : IEntityRepository<Product>
    {
    }
}
