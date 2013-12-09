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
            node = Syntax.InvocationExpression(
                Syntax.IdentifierName("Must"),
                Syntax.ArgumentList(
                    Syntax.SeparatedList(
                        Syntax.Argument(
                            Syntax.IdentifierName("BeAwesome")))));

            documenter = new PredicateDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).ShouldEqual("Must Be Awesome");

        static PredicateDocumenter documenter;

        static SyntaxNode node;
    }
}