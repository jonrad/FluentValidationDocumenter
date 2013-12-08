using System.Linq;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(RuleForMethodDocumenter))]
    public class RuleForMethodDocumenterSpecs
    {
        Establish context = () =>
        {
            method = SyntaxTree.ParseText("RuleFor(m => m.Name)").GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            documenter = new RuleForMethodDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(method).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(method).ShouldEqual("Name");

        static IMethodDocumenter documenter;

        static MethodDeclarationSyntax method;
    }
}