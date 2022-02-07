using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.Address
{
    public class AddressUpdateValidator : AbstractValidator<AddressUpdateDto>
    {
        public AddressUpdateValidator()
        {
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.District).NotEmpty();
            RuleFor(a => a.Description).NotEmpty();
            RuleFor(a => a.Id).NotEmpty().GreaterThan(0);
        }
    }
}
