using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.Customer
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(c => c.FullName).NotEmpty();
            RuleFor(c => c.Id).NotEmpty().GreaterThan(0);
        }
    }
}
