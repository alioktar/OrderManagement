using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Services.Base
{
    public interface ICustomerService
    {
        Task<IDataResponse<CustomerDto>> GetAsync(int customerId);
        Task<IDataResponse<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<IDataResponse<CustomerWithOrdersDto>> GetCustomerWithOrdersAsync(int customerId);

        Task<IDataResponse<CustomerDto>> CreateAsync(CustomerAddDto customer);
        Task<IDataResponse<CustomerDto>> UpdateAsync(CustomerUpdateDto customer);
        Task<IResponse> DeleteAsync(int customerId);

        Task<IDataResponse<CustomerDto>> AddCustomerAddressAsync(int customerId, AddressAddDto address);
        Task<IDataResponse<CustomerDto>> UpdateCustomerAddressAsync(int customerId, AddressUpdateDto address);
        Task<IResponse> DeleteCustomerAddressAsync(int customerId, int addressId);
    }
}
