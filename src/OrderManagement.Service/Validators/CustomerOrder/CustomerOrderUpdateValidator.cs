using FluentValidation;
using OrderManagement.DTOs;
using OrderManagement.Service.Validators.Address;

namespace OrderManagement.Service.Validators.CustomerOrder
{
    public class CustomerOrderUpdateValidator : AbstractValidator<OrderUpdateDto>
    {
        public CustomerOrderUpdateValidator()
        {
            RuleFor(o => o.ShippingAddress).NotEmpty().SetValidator(new AddressAddValidator());
            RuleForEach(o => o.Products).NotEmpty().ChildRules(products =>
            {
                products.RuleFor(p => p.Quantity).NotEmpty().GreaterThan(0);
                products.RuleFor(p => p.ProductId).NotEmpty().GreaterThan(0);
            });
        }
    }
}
