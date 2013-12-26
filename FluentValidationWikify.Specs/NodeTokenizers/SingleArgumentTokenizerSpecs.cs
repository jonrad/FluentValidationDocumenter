using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    public class SingleArgumentTokenizerSpecs
    {
        [Subject(typeof(SingleArgumentTokenizer))]
        public class when_processing
        {
            Establish context = () =>
            {
                node =
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.IdentifierName("x"),
                            Syntax.IdentifierName("Equals")),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList(
                                Syntax.Argument(
                                    Syntax.LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Syntax.Literal("1.23m", 1.23m))))));

                documenter = new SingleArgumentTokenizer();
            };

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_equals = () =>
                documenter.Get(node).Id.ShouldEqual("equals");

            It should_have_what_it_equals = () =>
                documenter.Get(node).Info.ShouldEqual(1.23m);

            static INodeTokenizer documenter;

            static SyntaxNode node;
        }
    }
}