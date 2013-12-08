using System;
using FluentValidation.Validators;

namespace FluentValidationWikify
{
    public class NotEmptyDocumenter : IPropertyDocumenter
    {
        public bool CanProcess(IPropertyValidator propertyValidator)
        {
            return propertyValidator is NotEmptyValidator;
        }

        public string Get(IPropertyValidator propertyValidator)
        {
            if (!CanProcess(propertyValidator))
            {
                throw new Exception();
            }

            return "Required";
        }
    }
}