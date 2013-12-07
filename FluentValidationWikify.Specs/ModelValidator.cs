using FluentValidation;

namespace FluentValidationWikify.Specs
{
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Name).Length(1, 5);
        }
    }
}