using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof (PredicateTokenizer))]
    public class PredicateTokenizerSpecs
    {

        [Subject(typeof (PredicateTokenizer))]
        private class in_simple_case
        {
            private Establish context = () =>
            {

                var lamda = Syntax.SimpleLambdaExpression(
                    Syntax.Parameter(Syntax.Identifier("t")),
                    Syntax.ObjectCreationExpression(Syntax.ParseTypeName("Person")));

                node =
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.IdentifierName("x"),
                            Syntax.IdentifierName("Must")),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList(
                                Syntax.Argument(
                                    Syntax.InvocationExpression(
                                        Syntax.IdentifierName("Exist"),
                                        Syntax.ArgumentList(
                                            Syntax.SeparatedList(
                                                Syntax.Argument(lamda))))))));

                documenter = new PredicateTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_must = () =>
                documenter.Get(node).Id.ShouldEqual("must");

            It should_return_be_aweomse = () =>
                documenter.Get(node).Info.ShouldEqual("Exist");

            private static PredicateTokenizer documenter;

        }

        private static SyntaxNode node;
    }
}