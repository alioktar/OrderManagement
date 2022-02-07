using OrderManagement.Core.Repository;
using OrderManagement.Entities;
using System.Linq.Expressions;

namespace OrderManagement.Repository.Base
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        Task<Customer?> GetWithIncludesAsync(Expression<Func<Customer, bool>> filter);
    }
}
