using System.Linq;
using FluentValidationWikify.Tokenizers;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class TextTokenizerSpecs
    {
        [Subject(typeof(TextTokenizer))]
        class when_called_with_single_class : WithSubject<TextTokenizer>
        {
            Because of = () =>
                Subject.Get("class ModelValidator:AbstractValidator<Model>{}").ToArray();

            It called_class_documenter_once = () =>
                The<IClassTokenizer>().WasToldTo(c => c.Get(Param.IsAny<ClassDeclarationSyntax>())).OnlyOnce();
        }

        [Subject(typeof(TextTokenizer))]
        class when_called_with_two_classes : WithSubject<TextTokenizer>
        {
            Because of = () =>
                Subject.Get("class ModelValidator:AbstractValidator<Model>{}class AnotherValidator:AbstractValidator<Another>{}").ToArray();

            private It called_class_documenter_once = () =>
                The<IClassTokenizer>().WasToldTo(c => c.Get(Param.IsAny<ClassDeclarationSyntax>())).Twice();
        }
    }
}