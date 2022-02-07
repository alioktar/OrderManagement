using Microsoft.EntityFrameworkCore;
using OrderManagement.Repository.Base;
using OrderManagement.Repository.EntityFreamework.Repositories;

namespace OrderManagement.Repository.EntityFreamework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => _productRepository = _productRepository ?? new ProductRepository(_context);
        public ICustomerRepository CustomerRepository => _customerRepository = _customerRepository ?? new CustomerRepository(_context);

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                RollbackEntityChanges();
                throw;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                await RollbackEntityChangesAsync();
                throw;
            }
        }

        protected void RollbackEntityChanges()
        {
            if (_context is DbContext dbContext)
            {
                RollbackEntityStates(dbContext);
                _context.SaveChanges();
            }
        }

        protected async Task RollbackEntityChangesAsync()
        {
            if (_context is DbContext dbContext)
            {
                RollbackEntityStates(dbContext);
                await _context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _productRepository = null;
            _customerRepository = null;
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            _productRepository = null;
            _customerRepository = null;
            await _context.DisposeAsync();
        }

        private void RollbackEntityStates(DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries()
                                .Where(e =>
                                        e.State == EntityState.Added ||
                                        e.State == EntityState.Modified ||
                                        e.State == EntityState.Deleted).ToList();

            entries.ForEach(entry =>
            {
                entry.State = EntityState.Unchanged;
            });
        }
    }
}
