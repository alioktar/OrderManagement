using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Repository.EntityFreamework;
using OrderManagement.Entities;
using OrderManagement.Repository.Base;
using System.Linq.Expressions;

namespace OrderManagement.Repository.EntityFreamework.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context) { }

        public async Task<Customer?> GetWithIncludesAsync(Expression<Func<Customer, bool>> filter)
        {
            return await Entities.Where(filter).Include(c => c.Orders)
                                                   .ThenInclude(o => o.Products)
                                                       .ThenInclude(op => op.Product)
                                               .Include(c => c.Orders)
                                                   .ThenInclude(o => o.ShippingAddress)
                                               .FirstOrDefaultAsync();
        }
    }
}
