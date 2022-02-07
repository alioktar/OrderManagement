using FluentValidation;
using OrderManagement.DTOs;
using OrderManagement.Service.Validators.Address;

namespace OrderManagement.Service.Validators.CustomerOrder
{
    public class CustomerOrderAddValidator : AbstractValidator<OrderAddDto>
    {
        public CustomerOrderAddValidator()
        {
            RuleFor(o => o.Customer).NotEmpty().NotNull().ChildRules(customer =>
            {
                customer.RuleFor(c => c.FullName).NotEmpty();
            });
            RuleFor(o => o.ShippingAddress).NotEmpty().SetValidator(new AddressAddValidator());
            RuleForEach(o => o.Products).NotEmpty().ChildRules(products =>
            {
                products.RuleFor(p => p.Quantity).NotEmpty().GreaterThan(0);
                products.RuleFor(p => p.ProductId).NotEmpty().GreaterThan(0);
            });
        }
    }
}
