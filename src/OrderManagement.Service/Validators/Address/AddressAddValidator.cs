using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.Address
{
    public class AddressAddValidator : AbstractValidator<AddressAddDto>
    {
        public AddressAddValidator()
        {
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.District).NotEmpty();
            RuleFor(a => a.Description).NotEmpty();
        }
    }
}
