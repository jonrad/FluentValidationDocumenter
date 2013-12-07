using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Machine.Specifications;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(PropertyValidatorPretify))]
    public class PropertyValidatorPretifySpecs
    {
        Establish context = () =>
        {
            var validator = new InlineValidator<Model>();
            validator.RuleFor(v => v.Name).NotEmpty();

            propertyValidator = validator.CreateDescriptor().GetValidatorsForMember("Name").First();

            pretify = new PropertyValidatorPretify();
        };

        Because of = () =>
            result = pretify.Get(propertyValidator);

        It should_return_expected_result =
            () => result.ShouldEqual("Required");

        static IPropertyValidator propertyValidator;

        static PropertyValidatorPretify pretify;

        static string result;
    }
}
