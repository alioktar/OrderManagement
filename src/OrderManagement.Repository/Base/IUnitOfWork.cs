namespace OrderManagement.Repository.Base
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public ICustomerRepository CustomerRepository { get;}
        public IProductRepository ProductRepository { get;}

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
