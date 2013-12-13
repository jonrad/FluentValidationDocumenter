using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof(WhenTokenizer))]
    public class WhenTokenizerSpecs
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

                documenter = new WhenTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_when = () =>
                documenter.Get(node).Id.ShouldEqual("When");

            It should_return_isawesome_for_info = () =>
                documenter.Get(node).Info.ShouldEqual("IsAwesome");

            static WhenTokenizer documenter;

            static SyntaxNode node;
        }
    }
}