using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(RuleForDocumenter))]
    public class RuleForDocumenterSpecs
    {
        Establish context = () =>
        {
            // RuleFor(m => m.Name)
            node = Syntax.InvocationExpression(
                Syntax.IdentifierName("RuleFor"),
                Syntax.ArgumentList(
                    Syntax.SeparatedList(
                        Syntax.Argument(
                            Syntax.SimpleLambdaExpression(
                                Syntax.Parameter(Syntax.Identifier("m")),
                                Syntax.MemberAccessExpression(
                                    SyntaxKind.MemberAccessExpression,
                                    Syntax.IdentifierName("m"),
                                    Syntax.IdentifierName("Name")))))));

            documenter = new RuleForDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).ShouldEqual("Name");

        static INodeDocumenter documenter;

        static SyntaxNode node;
    }
}