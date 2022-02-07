using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.Customer
{
    public class CustomerAddValidator: AbstractValidator<CustomerAddDto>
    {
        public CustomerAddValidator()
        {
            RuleFor(c => c.FullName).NotEmpty();
            RuleFor(c => c.Addresses).NotNull().NotEmpty();
            RuleForEach(c => c.Addresses).ChildRules(addresses =>
            {
                addresses.RuleFor(a => a.City).NotEmpty();
                addresses.RuleFor(a => a.District).NotEmpty();
                addresses.RuleFor(a => a.Description).NotEmpty();
            });
        }
    }
}
