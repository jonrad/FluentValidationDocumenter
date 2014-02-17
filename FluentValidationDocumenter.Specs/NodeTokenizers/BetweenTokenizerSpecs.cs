using FluentValidationDocumenter.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Specs.NodeTokenizers
{
    public class BetweenTokenizerSpecs
    {
        [Subject(typeof(MultipleArgumentTokenizer))]
        public class when_processing_inclusive
        {
            Establish context = () =>
            {
                node =
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.IdentifierName("x"),
                            Syntax.IdentifierName("InclusiveBetween")),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList<ArgumentSyntax>(
                                Syntax.Argument(
                                    Syntax.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Syntax.Literal("1", 1))),
                                Syntax.Token(SyntaxKind.CommaToken),
                                Syntax.Argument(
                                    Syntax.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Syntax.Literal("2", 2)))
                                )));

                documenter = new MultipleArgumentTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_correct_id = () =>
                documenter.Get(node).Id.ShouldEqual("inclusivebetween");

            It should_have_arguments = () =>
                ((object[]) documenter.Get(node).Info).ShouldContainOnly(new object[] {1, 2});

            static InvocationExpressionSyntax node;

            static MultipleArgumentTokenizer documenter;
        }

        [Subject(typeof(MultipleArgumentTokenizer))]
        public class when_processing_exclusive
        {
            Establish context = () =>
            {
                node =
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.IdentifierName("x"),
                            Syntax.IdentifierName("ExclusiveBetween")),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList<ArgumentSyntax>(
                                Syntax.Argument(
                                    Syntax.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Syntax.Literal("1", 1))),
                                Syntax.Token(SyntaxKind.CommaToken),
                                Syntax.Argument(
                                    Syntax.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Syntax.Literal("2", 2)))
                                )));

                documenter = new MultipleArgumentTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_correct_id = () =>
                documenter.Get(node).Id.ShouldEqual("exclusivebetween");

            It should_have_arguments = () =>
                ((object[]) documenter.Get(node).Info).ShouldContainOnly(new object[] {1, 2});

            static InvocationExpressionSyntax node;

            static MultipleArgumentTokenizer documenter;
        }
    }
}