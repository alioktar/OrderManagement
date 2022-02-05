using AutoMapper;
using OrderManagement.Core.CrossCuttingConcerns.Caching;
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
    }
}
