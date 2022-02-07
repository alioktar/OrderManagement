using AutoMapper;
using OrderManagement.Core.Aspects.Autofac.Caching;
using OrderManagement.Core.Aspects.Autofac.Validation;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
using OrderManagement.Core.Utilities.Exceptions;
using OrderManagement.Core.Utilities.Response;
using OrderManagement.DTOs;
using OrderManagement.Entities;
using OrderManagement.Repository.Base;
using OrderManagement.Service.Core;
using OrderManagement.Service.Services.Base;
using OrderManagement.Service.Validators.Address;
using OrderManagement.Service.Validators.Customer;
using System.Linq.Expressions;

namespace OrderManagement.Service.Services.Concrete
{
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ICacheManager cacheManager) : base(unitOfWork, mapper, cacheManager) { }

        public async Task<IDataResponse<CustomerDto>> GetAsync(int customerId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(
                c => c.Id == customerId,
                new Expression<Func<Customer, object>>[] {
                    c => c.Addresses,
                    c => c.Addresses.Where(a => !a.IsDeleted)
                }) ?? throw new NotFoundException($"Customer not found with id : {customerId}");

            return new SuccessDataResponse<CustomerDto>(Mapper.Map<CustomerDto>(customer));
        }

        [CacheAspect(type:typeof(IDataResponse<IEnumerable<CustomerDto>>))]
        public async Task<IDataResponse<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var customers = await UnitOfWork.CustomerRepository.GetListAsync(
                c => !c.IsDeleted,
                new Expression<Func<Customer, object>>[] {
                    c => c.Addresses,
                    c => c.Addresses.Where(a => !a.IsDeleted)
                });

            return new SuccessDataResponse<IEnumerable<CustomerDto>>(Mapper.Map<IEnumerable<CustomerDto>>(customers));
        }

        public async Task<IDataResponse<CustomerWithOrdersDto>> GetCustomerWithOrdersAsync(int customerId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetWithIncludesAsync(c => c.Id == customerId) ?? throw new NotFoundException($"Customer not found with id : {customerId}");
            var response = new CustomerWithOrdersDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Addresses = Mapper.Map<List<AddressDto>>(customer.Addresses),
                Orders= new List<OrderDto>(),
            };

            foreach (var order in customer.Orders)
            {
                response.Orders.Add(new OrderDto
                {
                    Id = order.Id,
                    OrderNumber = order.OrderNumber,
                    Customer = Mapper.Map<CustomerOrderDto>(customer),
                    ShippingAddress = Mapper.Map<AddressDto>(order.ShippingAddress),
                    Products = Mapper.Map<List<ProductDto>>(order.Products.Select(x => x.Product).ToList()),
                });
            }

            return new SuccessDataResponse<CustomerWithOrdersDto>(response);
        }

        [ValidationAspect(typeof(CustomerAddValidator))]
        public async Task<IDataResponse<CustomerDto>> CreateAsync(CustomerAddDto customer)
        {
            var added = await UnitOfWork.CustomerRepository.AddAsync(Mapper.Map<Customer>(customer));
            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<CustomerDto>(Mapper.Map<CustomerDto>(added));
        }

        [ValidationAspect(typeof(CustomerUpdateValidator))]
        public async Task<IDataResponse<CustomerDto>> UpdateAsync(CustomerUpdateDto customer)
        {
            var exists = await UnitOfWork.CustomerRepository.GetAsync(c => c.Id == customer.Id) ?? throw new NotFoundException($"Customer not found with id : {customer.Id}");

            var updated = UnitOfWork.CustomerRepository.Update(Mapper.Map(customer, exists));
            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<CustomerDto>(Mapper.Map<CustomerDto>(updated));
        }

        public async Task<IResponse> DeleteAsync(int customerId)
        {
            var exists = await UnitOfWork.CustomerRepository.GetAsync(c => c.Id == customerId) ?? throw new NotFoundException($"Customer not found with id : {customerId}");
            UnitOfWork.CustomerRepository.Delete(exists);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessResponse();
        }

        [ValidationAspect(typeof(AddressAddValidator))]
        public async Task<IDataResponse<CustomerDto>> AddCustomerAddressAsync(int customerId, AddressAddDto address)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(
                c => c.Id == customerId,
                new Expression<Func<Customer, object>>[] {
                    c => c.Addresses,
                    c => c.Addresses.Where(a => !a.IsDeleted)
                }) ?? throw new NotFoundException($"Customer not found with id : {customerId}");

            customer.Addresses.Add(Mapper.Map<Address>(address));
            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<CustomerDto>(Mapper.Map<CustomerDto>(customer));
        }

        [ValidationAspect(typeof(CustomerUpdateValidator))]
        public async Task<IDataResponse<CustomerDto>> UpdateCustomerAddressAsync(int customerId, AddressUpdateDto address)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(
                c => c.Id == customerId,
                new Expression<Func<Customer, object>>[] {
                    c => c.Addresses,
                    c => c.Addresses.Where(a => !a.IsDeleted)
                }) ?? throw new NotFoundException($"Customer not found with id : {customerId}");

            Address exists = customer.Addresses.FirstOrDefault(a => a.Id == address.Id) ?? throw new NotFoundException($"Address not found with id : {address.Id}");

            exists = Mapper.Map(address, exists);

            var updated = UnitOfWork.CustomerRepository.Update(customer);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessDataResponse<CustomerDto>(Mapper.Map<CustomerDto>(exists));
        }

        public async Task<IResponse> DeleteCustomerAddressAsync(int customerId, int addressId)
        {
            var customer = await UnitOfWork.CustomerRepository.GetAsync(
                c => c.Id == customerId,
                new Expression<Func<Customer, object>>[] {
                    c => c.Addresses,
                    c => c.Addresses.Where(a => !a.IsDeleted)
                }) ?? throw new NotFoundException($"Customer not found with id : {customerId}");

            Address address = customer.Addresses.FirstOrDefault(a => a.Id == addressId) ?? throw new NotFoundException($"Address not found with id : {addressId}");
            customer.Addresses.Remove(address);

            UnitOfWork.CustomerRepository.Update(customer);
            await UnitOfWork.SaveChangesAsync();

            return new SuccessResponse();
        }
    }
}
