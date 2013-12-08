using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Machine.Specifications;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(NotEmptyDocumenter))]
    public class NotEmptyDocumenterSpecs
    {
        Establish context = () =>
        {
            var validator = new InlineValidator<Model>();
            validator.RuleFor(v => v.Name).NotEmpty();

            propertyValidator = validator.CreateDescriptor().GetValidatorsForMember("Name").First();

            documenter = new NotEmptyDocumenter();
        };

        Because of = () =>
        {
            canProcess = documenter.CanProcess(propertyValidator);
            result = documenter.Get(propertyValidator);
        };

        It should_be_able_to_process =
            () => canProcess.ShouldBeTrue();


        It should_return_required =
            () => result.ShouldEqual("Required");

        static IPropertyValidator propertyValidator;

        static NotEmptyDocumenter documenter;

        static string result;

        static bool canProcess;
    }
}
