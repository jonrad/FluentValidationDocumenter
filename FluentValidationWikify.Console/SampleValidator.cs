using FluentValidation;

namespace FluentValidationWikify.Console
{
    public class SampleValidator : AbstractValidator<Person>
    {
        public SampleValidator()
        {
            RuleFor(t => t.Name).NotNull();
            RuleFor(t => t.Age).GreaterThanOrEqualTo(0);
            RuleFor(t => t.Age).InclusiveBetween(0, 150);
        }
    }
}
