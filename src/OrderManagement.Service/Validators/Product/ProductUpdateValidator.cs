using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.Product
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(p => p.Barcode).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Id).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
        }
    }
}
