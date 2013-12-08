using System.Linq;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(ClassDocumenter))]
    public class ClassDocumenterSpecs
    {
        Establish context = () =>
        {
            node = SyntaxTree.ParseText("class ModelValidator:AbstractValidator<Model>{}").GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

            documenter = new ClassDocumenter();
        };

        It should_be_able_to_process =
            () => documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required =
            () => documenter.Get(node).Name.ShouldEqual("Model");

        static ClassDocumenter documenter;

        static ClassDeclarationSyntax node;
    }
}