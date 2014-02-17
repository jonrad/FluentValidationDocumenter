using FluentValidation;

namespace FluentValidationWikify.Console.Sample
{
    public class SampleValidator : AbstractValidator<Person>
    {
        public SampleValidator()
        {
            When(
                p => p.Age > 25,
                () =>
                {
                    RuleFor(t => t.Name).NotNull();
                });
        }
    }
}
