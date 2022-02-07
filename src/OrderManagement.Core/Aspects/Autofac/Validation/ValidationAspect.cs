using Castle.DynamicProxy;
using FluentValidation;
using OrderManagement.Core.CrossCuttingConcerns.Validation;
using OrderManagement.Core.Utilities.Interceptors;

namespace OrderManagement.Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly IValidator _validator;

        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new ArgumentException("Wrong Validation Type");
            }

            _validator = Activator.CreateInstance(validatorType) as IValidator ?? throw new InvalidOperationException("Argument Type Instance Can't Created!");
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var entityType = _validator.GetType().BaseType?.GetGenericArguments()[0];

            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(_validator, entity);
            }
        }
    }
}
