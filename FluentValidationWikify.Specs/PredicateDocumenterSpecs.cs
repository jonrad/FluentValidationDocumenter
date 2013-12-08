using System.Linq;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(PredicateDocumenter))]
    public class PredicateDocumenterSpecs
    {
        Establish context = () =>
        {
            method = SyntaxTree.ParseText("Must(BeAwesome)").GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            documenter = new PredicateDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(method).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(method).ShouldEqual("Must Be Awesome");

        static PredicateDocumenter documenter;

        static MethodDeclarationSyntax method;
    }
}