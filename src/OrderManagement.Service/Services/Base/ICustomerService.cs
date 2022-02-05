using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Services.Base
{
    public interface ICustomerService
    {
        Task<IDataResponse<CustomerDto>> Get(int customerId);
        Task<IDataResponse<CustomerWithOrdersDto>> GetCustomerWithProducts(int id);
        Task<IDataResponse<CustomerDto>> Get(int customerId);

        Task<IDataResponse<CustomerDto>> Create(CustomerAddDto customer);
        Task<IDataResponse<CustomerDto>> Update(CustomerUpdateDto customer);
        Task<IResponse> Delete(int customerId);

        Task<IDataResponse<CustomerDto>> AddCustomerAddress(int customerId, AddressAddDto address);
        Task<IDataResponse<CustomerDto>> UpdateCustomerAddress(int customerId, AddressUpdateDto address);
        Task<IResponse> DeleteCustomerAddress(int customerId, int addressId);

        Task<IDataResponse<OrderDto>> GetOrder(int customerId);
        Task<IDataResponse<IEnumerable<OrderDto>>> GetOrders(int customerId);

        Task<IResponse> AddOrder(int customerId);
        Task<IResponse> UpdateOrder(int customerId);
        Task<IResponse> DeleteOrder(int customerId);
        Task<IResponse> CancelOrder(int customerId);
    }
}
