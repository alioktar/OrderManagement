using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.Product
{
    public class ProductAddValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddValidator()
        {
            RuleFor(p => p.Barcode).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p=> p.Price).NotEmpty().GreaterThan(0);
        }
    }
}
