using FluentValidation;
using OrderManagement.DTOs;

namespace OrderManagement.Service.Validators.CustomerOrder
{
    public class ExistsCustomerOrderAddValidator : AbstractValidator<ExistsCustomerOrderAddDto>
    {
        public ExistsCustomerOrderAddValidator()
        {
            RuleForEach(o => o.Products).NotEmpty().ChildRules(products =>
            {
                products.RuleFor(p => p.Quantity).NotEmpty().GreaterThan(0);
                products.RuleFor(p => p.ProductId).NotEmpty().GreaterThan(0);
            });
        }
    }
}
