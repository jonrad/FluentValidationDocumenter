using FluentValidation;

namespace FluentValidationWikify
{
    public class Model
    {
        public string Name { get; set; }
    }

    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
        }
    }
}