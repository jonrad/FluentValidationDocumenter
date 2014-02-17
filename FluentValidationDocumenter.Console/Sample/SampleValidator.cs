using FluentValidation;

namespace FluentValidationDocumenter.Console.Sample
{
    public class SampleValidator : AbstractValidator<Person>
    {
        public SampleValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Adjective).Must(BeWise).When(PersonIsJon);
            When(
                p => p.Age > 25,
                () =>
                {
                    RuleFor(t => t.Adjective).NotEmpty();
                    RuleFor(t => t.Adjective).Must(a => a.Length > 5);
                });
        }

        private bool PersonIsJon(Person arg)
        {
            return arg.Name == "Jon";
        }

        private bool BeWise(string arg)
        {
            return arg == "Wise";
        }
    }
}
