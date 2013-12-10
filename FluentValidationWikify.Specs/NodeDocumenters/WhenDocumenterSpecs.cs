using FluentValidationWikify.NodeDocumenters;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeDocumenters
{
    [Subject(typeof(WhenDocumenter))]
    public class WhenDocumenterSpecs
    {
        private class when_using_a_predicate
        {
            private Establish context = () =>
            {
                node =
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.IdentifierName("x"),
                            Syntax.IdentifierName("When")),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList(
                                Syntax.Argument(
                                    Syntax.IdentifierName("IsAwesome")))));

                documenter = new WhenDocumenter();
            };

            private It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            private It should_return_required = () =>
                documenter.Get(node).ShouldEqual("When Is Awesome");

            private static WhenDocumenter documenter;

            private static SyntaxNode node;
        }
    }
}