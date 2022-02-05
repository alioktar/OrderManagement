using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Repository.EntityFreamework;
using OrderManagement.Entities;
using OrderManagement.Repository.Base;

namespace OrderManagement.Repository.EntityFreamework.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context) { }
    }
}
