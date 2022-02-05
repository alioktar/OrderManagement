using OrderManagement.HttpApi.Base;
using OrderManagement.Service.Services.Base;

namespace OrderManagement.HttpApi.Customer
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
    }
}
