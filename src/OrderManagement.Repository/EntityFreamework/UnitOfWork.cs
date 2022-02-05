using Microsoft.EntityFrameworkCore;
using OrderManagement.Repository.Base;

namespace OrderManagement.Repository.EntityFreamework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private ICustomerRepository _customerRepository;

        public UnitOfWork(DbContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        public ICustomerRepository CustomerRepository => _customerRepository;

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
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
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
