using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Machine.Specifications;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(PredicateDocumenter))]
    public class PredicateDocumenterSpecs
    {
        Establish context = () =>
        {
            var validator = new InlineValidator<Model>();
            validator.RuleFor(v => v.Name).Must(BeAwesome);

            propertyValidator = validator.CreateDescriptor().GetValidatorsForMember("Name").First();

            documenter = new PredicateDocumenter();
        };

        static bool BeAwesome(string arg)
        {
            return arg == "Jon";
        }

        It should_be_able_to_process =
            () => documenter.CanProcess(propertyValidator).ShouldBeTrue();

        It should_return_required =
            () => documenter.Get(propertyValidator).ShouldEqual("Must Be Awesome");

        static IPropertyValidator propertyValidator;

        static PredicateDocumenter documenter;
    }
}