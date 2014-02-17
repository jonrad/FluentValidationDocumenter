using FluentValidationWikify.Models;
using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    public class WhenClosureTokenizerSpecs
    {
        [Subject(typeof(WhenClosureTokenizer))]
        public class when_processing_when_extension_method
        {
            Establish context = () =>
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

                tokenizer = new WhenClosureTokenizer();
            };

            It should_be_able_to_process = () =>
                tokenizer.CanProcess(node).ShouldBeFalse();

            static INodeTokenizer tokenizer;

            static SyntaxNode node;
        }

        [Subject(typeof(WhenClosureTokenizer))]
        public class when_processing_proper_method
        {
            Establish context = () =>
            {
                block = Syntax.Block(Syntax.ExpressionStatement(Tokens.RuleForName));
                node =
                    Syntax.InvocationExpression(
                        Syntax.IdentifierName("When"),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList<ArgumentSyntax>(
                                Syntax.Argument(Tokens.AgeLessThan25),
                                Syntax.Token(SyntaxKind.CommaToken),
                                Syntax.Argument(
                                    Syntax.ParenthesizedLambdaExpression(
                                        Syntax.ParameterList(),
                                        block))))); 

                tokenizer = new WhenClosureTokenizer();
            };

            Because of = () =>
                results = tokenizer.Get(node);

            It should_be_able_to_process = () =>
                tokenizer.CanProcess(node).ShouldBeTrue();

            It should_be_of_type_when_closure = () =>
                results.Id.ShouldEqual("WhenClosure");

            It should_return_proper_details_type = () =>
                results.Info.ShouldBeOfType<WhenClosureDetails>();

            It should_have_a_when_clause_as_expected = () =>
                ((WhenClosureDetails) results.Info).WhenDetails.ToString().ShouldEqual(Tokens.AgeLessThan25.ToString());

            It should_have_a_block_in_details = () =>
                ((WhenClosureDetails) results.Info).Block.ToString().ShouldEqual(block.ToString());

            static INodeTokenizer tokenizer;

            static SyntaxNode node;

            static Token results;

            static BlockSyntax block;
        }
    }
}