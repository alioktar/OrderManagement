using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Repository.EntityFreamework;
using OrderManagement.Entities;
using OrderManagement.Repository.Base;

namespace OrderManagement.Repository.EntityFreamework.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context) { }
    }
}
