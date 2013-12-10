using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class TextDocumenterSpecs
    {
        [Subject(typeof(TextDocumenter))]
        class when_called_with_single_class : WithSubject<TextDocumenter>
        {
            Because of = () =>
                Subject.Get("class ModelValidator:AbstractValidator<Model>{}").ToArray();

            It called_class_documenter_once = () =>
                The<IClassDocumenter>().WasToldTo(c => c.Get(Param.IsAny<ClassDeclarationSyntax>())).OnlyOnce();
        }

        [Subject(typeof(TextDocumenter))]
        class when_called_with_two_classes : WithSubject<TextDocumenter>
        {
            Because of = () =>
                Subject.Get("class ModelValidator:AbstractValidator<Model>{}class AnotherValidator:AbstractValidator<Another>{}").ToArray();

            private It called_class_documenter_once = () =>
                The<IClassDocumenter>().WasToldTo(c => c.Get(Param.IsAny<ClassDeclarationSyntax>())).Twice();
        }
    }
}