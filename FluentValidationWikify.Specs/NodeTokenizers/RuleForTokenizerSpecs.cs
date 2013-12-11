using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof(RuleForTokenizer))]
    public class RuleForTokenizerSpecs
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

            documenter = new RuleForTokenizer();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).Info.ShouldEqual("Name");

        static INodeTokenizer documenter;

        static SyntaxNode node;
    }
}