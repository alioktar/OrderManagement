using Microsoft.AspNetCore.Mvc;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.HttpApi.Base;
using OrderManagement.Service.Services.Base;

namespace OrderManagement.HttpApi.CustomerOrder
{
    public class CustomerOrdersController : BaseController
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CustomerOrdersController(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        [HttpPost]
        public async Task<IDataResponse<string>> AddOrder(OrderAddDto order) => await _customerOrderService.AddOrderAsync(order);

        [HttpPost("{customerId}/orders")]
        public async Task<IDataResponse<string>> AddOrderToExistingCustomer(int customerId, ExistsCustomerOrderAddDto orderAddDto) => await _customerOrderService.AddOrderToExistingCustomer(customerId, orderAddDto);

        [HttpPost("{customerId}/orders/{orderId}/cancel")]
        public async Task<IResponse> CancelOrder(int customerId, int orderId) => await _customerOrderService.CancelOrderAsync(customerId, orderId);

        [HttpDelete("{customerId}/orders/{orderId}")]
        public async Task<IResponse> DeleteOrder(int customerId, int orderId) => await _customerOrderService.DeleteOrderAsync(customerId, orderId);

        [HttpGet("{customerId}/orders/{orderId}")]
        public async Task<IDataResponse<OrderDto>> GetOrder(int customerId, int orderId) => await _customerOrderService.GetOrderAsync(customerId, orderId);

        [HttpGet("{customerId}/orders/")]
        public async Task<IDataResponse<IEnumerable<OrderDto>>> GetOrders(int customerId) => await _customerOrderService.GetOrdersAsync(customerId);

        [HttpPut("{customerId}/orders/{orderId}")]
        public async Task<IDataResponse<string>> UpdateOrder(int customerId, int orderId, OrderUpdateDto order) => await _customerOrderService.UpdateOrderAsync(customerId, orderId, order);
    }
}
