using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof(PredicateTokenizer))]
    public class PredicateTokenizerSpecs
    {
        Establish context = () =>
        {
            node = 
                Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.IdentifierName("x"),
                        Syntax.IdentifierName("Must")),
                    Syntax.ArgumentList(
                    Syntax.SeparatedList(
                        Syntax.Argument(
                            Syntax.IdentifierName("BeAwesome")))));

            documenter = new PredicateTokenizer();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_must = () =>
            documenter.Get(node).Id.ShouldEqual("Must");

        It should_return_be_aweomse = () =>
            documenter.Get(node).Info.ShouldEqual("BeAwesome");

        static PredicateTokenizer documenter;

        static SyntaxNode node;
    }
}