using FluentValidation.Validators;

namespace FluentValidationWikify
{
    public interface IPropertyDocumenter
    {
        bool CanProcess(IPropertyValidator propertyValidator);

        string Get(IPropertyValidator propertyValidator);
    }
}