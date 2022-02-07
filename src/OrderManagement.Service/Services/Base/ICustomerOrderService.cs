using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Services.Base
{
    public interface ICustomerOrderService
    {
        Task<IDataResponse<OrderDto>> GetOrderAsync(int customerId, int orderId);
        Task<IDataResponse<IEnumerable<OrderDto>>> GetOrdersAsync(int customerId);


        Task<IDataResponse<string>> AddOrderAsync(OrderAddDto order);
        Task<IDataResponse<string>> AddOrderToExistingCustomer(int customerId, ExistsCustomerOrderAddDto orderAddDto);
        Task<IDataResponse<string>> UpdateOrderAsync(int customerId, int orderId, OrderUpdateDto order);
        Task<IResponse> DeleteOrderAsync(int customerId, int orderId);
        Task<IResponse> CancelOrderAsync(int customerId, int orderId);

    }
}
