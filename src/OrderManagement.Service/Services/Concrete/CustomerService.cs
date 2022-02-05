using AutoMapper;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.Repository.Base;
using OrderManagement.Service.Core;
using OrderManagement.Service.Services.Base;

namespace OrderManagement.Service.Services.Concrete
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager) : base(unitOfWork, mapper, cacheManager) { }

        public Task<IDataResponse<CustomerDto>> AddCustomerAddress(int customerId, AddressAddDto address)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> AddOrder(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> CancelOrder(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<CustomerDto>> Create(CustomerAddDto customer)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> Delete(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> DeleteCustomerAddress(int customerId, int addressId)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> DeleteOrder(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<CustomerDto>> Get(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<IEnumerable<CustomerDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<CustomerWithOrdersDto>> GetCustomerWithProducts(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<OrderDto>> GetOrder(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<IEnumerable<OrderDto>>> GetOrders(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<CustomerDto>> Update(CustomerUpdateDto customer)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResponse<CustomerDto>> UpdateCustomerAddress(int customerId, AddressUpdateDto address)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse> UpdateOrder(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
