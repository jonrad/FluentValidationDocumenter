using FluentValidation;

namespace FluentValidationWikify.Console.Sample
{
    public class SampleValidator : AbstractValidator<Person>
    {
        public SampleValidator()
        {
            RuleFor(t => t.Name).Must(n => n == "Jon");
            /*When(
                p => p.Age > 25,
                () =>
                {
                    RuleFor(t => t.Name).NotNull();
                    RuleFor(t => t.Age).GreaterThanOrEqualTo(0);
                });*/
        }
    }
}
