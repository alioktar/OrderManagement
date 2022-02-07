using AutoMapper;
using OrderManagement.Core.Aspects.Autofac.Validation;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.Utilities.Exceptions;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.Entities;
using OrderManagement.Repository.Base;
using OrderManagement.Service.Core;
using OrderManagement.Service.Services.Base;
using OrderManagement.Service.Validators.CustomerOrder;
using System.Linq.Expressions;

namespace OrderManagement.Service.Services.Concrete
{
    public class CustomerOrderService : BaseService, ICustomerOrderService
    {
        public CustomerOrderService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public CustomerOrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public CustomerOrderService(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager) : base(unitOfWork, mapper, cacheManager) { }

        public async Task<IDataResponse<OrderDto>> GetOrderAsync(int customerId, int orderId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetWithIncludesAsync(c => c.Id == customerId) ?? throw new NotFoundException($"Customer not found with id : {customerId}");

            Order order = customer.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new NotFoundException($"Order not found with id : {orderId}"); ;

            var response = new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Customer = Mapper.Map<CustomerOrderDto>(customer),
                ShippingAddress = Mapper.Map<AddressDto>(order.ShippingAddress),
                Products = Mapper.Map<List<ProductDto>>(order.Products.Select(x => x.Product).ToList()),
            };

            return new SuccessDataResponse<OrderDto>(response);
        }

        public async Task<IDataResponse<IEnumerable<OrderDto>>> GetOrdersAsync(int customerId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetWithIncludesAsync(c => c.Id == customerId) ?? throw new NotFoundException($"Customer not found with id : {customerId}");
            var response = new List<OrderDto>();

            foreach (var order in customer.Orders)
            {
                response.Add(new OrderDto
                {
                    Id=order.Id,
                    OrderNumber=order.OrderNumber,
                    Customer= Mapper.Map<CustomerOrderDto>(customer),
                    ShippingAddress = Mapper.Map<AddressDto>(order.ShippingAddress),
                    Products=Mapper.Map<List<ProductDto>>(order.Products.Select(x => x.Product).ToList()),
                });
            }

            return new SuccessDataResponse<IEnumerable<OrderDto>>(response);
        }

        [ValidationAspect(typeof(CustomerOrderAddValidator))]
        public async Task<IDataResponse<string>> AddOrderAsync(OrderAddDto orderAddDto)
        {
            var order = Mapper.Map<Order>(orderAddDto);
            order.OrderNumber = Guid.NewGuid().ToString();

            var customer = default(Customer);

            foreach (var orderProducts in orderAddDto.Products)
            {
                var product = await UnitOfWork.ProductRepository.GetAsync(
                        p => p.Id == orderProducts.ProductId && !p.IsDeleted
                    ) ?? throw new NotFoundException($"Product not found with id : {orderProducts.ProductId}");
            }

            if (order.Customer != null && order.ShippingAddress != null)
            {
                customer = order.Customer;
                customer.Addresses.Add(order.ShippingAddress);
                customer.Orders.Add(order);

                await UnitOfWork.CustomerRepository.AddAsync(customer);
            }
            else
                return new ErrorDataResponse<string>();

            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<string>(order.OrderNumber);
        }

        [ValidationAspect(typeof(ExistsCustomerOrderAddValidator))]
        public async Task<IDataResponse<string>> AddOrderToExistingCustomer(int customerId, ExistsCustomerOrderAddDto orderAddDto)
        {
            var order = Mapper.Map<Order>(orderAddDto);
            order.OrderNumber = Guid.NewGuid().ToString();

            var customer = await UnitOfWork.CustomerRepository.GetAsync(
            c => c.Id == customerId,
            new Expression<Func<Customer, object>>[] {
                    c => c.Addresses,
                    c => c.Addresses.Where(a => !a.IsDeleted)
            }) ?? throw new NotFoundException($"Customer not found with id : {order.CustomerId}");

            foreach (var orderProducts in orderAddDto.Products)
            {
                var product = await UnitOfWork.ProductRepository.GetAsync(
                        p => p.Id == orderProducts.ProductId && !p.IsDeleted
                    ) ?? throw new NotFoundException($"Product not found with id : {orderProducts.ProductId}");
            }

            order.Customer = customer;

            if (order.AddressId != 0)
            {
                order.ShippingAddress = customer.Addresses.FirstOrDefault(a => a.Id == order.AddressId) ?? throw new NotFoundException($"Address not found with id : {order.AddressId}");
            }
            else if (order.ShippingAddress != null)
            {
                customer.Addresses.Add(order.ShippingAddress);
            }
            else
                return new ErrorDataResponse<string>();

            customer.Orders.Add(order);
            UnitOfWork.CustomerRepository.Update(customer);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<string>(order.OrderNumber);
        }

        [ValidationAspect(typeof(CustomerOrderUpdateValidator))]
        public async Task<IDataResponse<string>> UpdateOrderAsync(int customerId, int orderId, OrderUpdateDto order)
        {
            var customer = await UnitOfWork.CustomerRepository.GetWithIncludesAsync(c => c.Id == customerId) ?? throw new NotFoundException($"Customer not found with id : {customerId}");
            var existsOrder = customer.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new NotFoundException($"Order not found with id : {orderId}");

            foreach (var orderProducts in order.Products)
            {
                var exists = existsOrder.Products.FirstOrDefault(p => p.ProductId == orderProducts.ProductId);
                if (exists == null)
                {
                    var product = await UnitOfWork.ProductRepository.GetAsync(p => p.Id == orderProducts.ProductId)
                        ?? throw new NotFoundException($"Product not found with id : {orderProducts.ProductId}");
                    existsOrder.Products.Add(new OrderProduct
                    {
                        Product = product,
                        Quantity = orderProducts.Quantity,
                        ProductId = orderProducts.ProductId,
                    });
                }
                else
                {
                    exists.Quantity = orderProducts.Quantity;
                }
            }

            existsOrder.ShippingAddress = Mapper.Map(order.ShippingAddress,existsOrder.ShippingAddress);

            UnitOfWork.CustomerRepository.Update(customer);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<string>(existsOrder.OrderNumber);
        }

        public async Task<IResponse> CancelOrderAsync(int customerId, int orderId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetWithIncludesAsync(c => c.Id == customerId) ?? throw new NotFoundException($"Customer not found with id : {customerId}");
            var existsOrder = customer.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new NotFoundException($"Order not found with id : {orderId}");
            existsOrder.IsCancelled = true;

            UnitOfWork.CustomerRepository.Update(customer);

            await UnitOfWork.SaveChangesAsync();

            return new SuccessResponse();
        }

        public async Task<IResponse> DeleteOrderAsync(int customerId, int orderId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(
                c => c.Id == customerId,
                new Expression<Func<Customer, object>>[] {
                    c => c.Orders
                }) ?? throw new NotFoundException($"Customer not found with id : {customerId}");

            var exists = customer.Orders.FirstOrDefault(o => o.Id == orderId) ?? throw new NotFoundException($"Order not found with id : {orderId}");
            customer.Orders.Remove(exists);

            UnitOfWork.CustomerRepository.Update(customer);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessResponse();

        }
    }
}
