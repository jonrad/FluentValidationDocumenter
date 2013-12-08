using System;
using FluentValidation.Validators;

namespace FluentValidationWikify
{
    public class PredicateDocumenter : IPropertyDocumenter
    {
        public bool CanProcess(IPropertyValidator propertyValidator)
        {
            return propertyValidator is PredicateValidator;
        }

        public string Get(IPropertyValidator propertyValidator)
        {
            var predicate = (PredicateValidator)propertyValidator;
            throw new NotImplementedException();
        }
    }
}