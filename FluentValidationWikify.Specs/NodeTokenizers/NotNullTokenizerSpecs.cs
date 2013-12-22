using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof(NotNullTokenizer))]
    public class NotNullTokenizerSpecs
    {
        Establish context = () =>
        {
            node = 
                Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.IdentifierName("x"),
                        Syntax.IdentifierName("NotNull")));

            documenter = new NotNullTokenizer();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).Id.ShouldEqual("required");

        static INodeTokenizer documenter;

        static SyntaxNode node;
    }
}
