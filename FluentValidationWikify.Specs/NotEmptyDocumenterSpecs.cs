using System.Linq;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(NotEmptyDocumenter))]
    public class NotEmptyDocumenterSpecs
    {
        Establish context = () =>
        {
            method = SyntaxTree.ParseText("NotNull()").GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            documenter = new NotEmptyDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(method).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(method).ShouldEqual("Required");

        static IMethodDocumenter documenter;

        static MethodDeclarationSyntax method;
    }
}
