using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.HttpApi.Base;
using OrderManagement.Service.Services.Base;

namespace OrderManagement.HttpApi.Customer
{
    public class CustomersController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("{customerId}/addresses")]
        public async Task<IDataResponse<CustomerDto>> AddCustomerAddress(int customerId, AddressAddDto address) => await _customerService.AddCustomerAddressAsync(customerId, address);

        [HttpPost]
        public async Task<IDataResponse<CustomerDto>> Create(CustomerAddDto customer) => await _customerService.CreateAsync(customer);

        [HttpDelete]
        public async Task<IResponse> Delete(int customerId) => await _customerService.DeleteAsync(customerId);

        [HttpDelete("{customerId}/addresses")]
        public async Task<IResponse> DeleteCustomerAddress(int customerId, int addressId) => await _customerService.DeleteCustomerAddressAsync(customerId, addressId);

        [HttpGet("{customerId}")]
        public async Task<IDataResponse<CustomerDto>> Get(int customerId) => await _customerService.GetAsync(customerId);

        [HttpGet]
        public async Task<IDataResponse<IEnumerable<CustomerDto>>> GetAll() => await _customerService.GetAllAsync();

        [HttpGet("/{customerId}/get-customer-with-orders")]
        public async Task<IDataResponse<CustomerWithOrdersDto>> GetCustomerWithOrders(int customerId) => await _customerService.GetCustomerWithOrdersAsync(customerId);

        [HttpPut]
        public async Task<IDataResponse<CustomerDto>> Update(CustomerUpdateDto customer) => await _customerService.UpdateAsync(customer);

        [HttpPut("{customerId}/addresses")]
        public async Task<IDataResponse<CustomerDto>> UpdateCustomerAddress(int customerId, AddressUpdateDto address) => await _customerService.UpdateCustomerAddressAsync(customerId, address);
    }
}
