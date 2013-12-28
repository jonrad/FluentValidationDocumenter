using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof(PredicateTokenizer))]
    public class PredicateTokenizerSpecs
    {
        [Subject(typeof(PredicateTokenizer))]
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

            private static SyntaxNode node;
        }

        private class when_using_when
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

                documenter = new PredicateTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_when = () =>
                documenter.Get(node).Id.ShouldEqual("when");

            It should_return_isawesome_for_info = () =>
                documenter.Get(node).Info.ShouldEqual("IsAwesome");

            static PredicateTokenizer documenter;

            static SyntaxNode node;
        }

        private class when_using_when_with_lamda
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
                                Syntax.Argument(lamda))));

                documenter = new PredicateTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_when = () =>
                documenter.Get(node).Id.ShouldEqual("when");

            It should_return_lamda_details_for_info = () =>
                documenter.Get(node).Info.ToString().ShouldEqual(lamda.ToString());

            static PredicateTokenizer documenter;

            static SyntaxNode node;

            static readonly SimpleLambdaExpressionSyntax lamda = Tokens.AgeLessThan25;
        }
    }
}