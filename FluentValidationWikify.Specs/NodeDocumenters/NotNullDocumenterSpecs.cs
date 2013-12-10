using FluentValidationWikify.NodeDocumenters;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeDocumenters
{
    [Subject(typeof(NotNullDocumenter))]
    public class NotNullDocumenterSpecs
    {
        Establish context = () =>
        {
            node = 
                Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.IdentifierName("x"),
                        Syntax.IdentifierName("NotNull")));

            documenter = new NotNullDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).ShouldEqual("Required");

        static INodeDocumenter documenter;

        static SyntaxNode node;
    }
}
