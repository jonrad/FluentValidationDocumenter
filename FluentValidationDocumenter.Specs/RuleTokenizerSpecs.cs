using System.Linq;
using FluentValidationWikify.Models;
using FluentValidationWikify.NodeTokenizers;
using FluentValidationWikify.Tokenizers;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class RuleTokenizerSpecs : WithFakes
    {
        [Subject(typeof(RuleTokenizer))]
        class for_single_rule : with_documenter
        {
            Establish context = () =>
            {
                // RuleFor(x => x.Name).Required()
                tree = Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Tokens.RuleForName,
                        Syntax.IdentifierName("Required")),
                    Syntax.ArgumentList());
            };

            Because of = () =>
            {
                rules = documenter.Get(tree).ToArray();
                rule = rules[0];
            };

            It should_return_a_single_rule = () =>
                rules.Length.ShouldEqual(1);

            It should_return_required = () =>
                rule.Name.ShouldEqual("Name");

            It should_return_a_single_detail = () =>
                rule.Details.Count().ShouldEqual(1);

            It should_have_a_required_detail = () =>
                rule.Details.First().Id.ShouldEqual("Required");

            private static InvocationExpressionSyntax tree;

            static Rule[] rules;

            static Rule rule;
        }

        [Subject(typeof(RuleTokenizer))]
        class for_single_rule_with_two_details : with_documenter
        {
            Establish context = () =>
            {
                // RuleFor(x => x.Name).Required().Cool()
                tree = Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.InvocationExpression(
                            Syntax.MemberAccessExpression(
                                SyntaxKind.MemberAccessExpression,
                                Tokens.RuleForName,
                                Syntax.IdentifierName("Required")),
                            Syntax.ArgumentList()),
                        Syntax.IdentifierName("Cool")),
                    Syntax.ArgumentList());
            };

            Because of = () =>
            {
                rules = documenter.Get(tree).ToArray();
                rule = rules[0];
            };

            It should_return_a_single_rule = () =>
                rules.Length.ShouldEqual(1);

            It should_return_required = () =>
                rule.Name.ShouldEqual("Name");

            It should_return_a_single_detail = () =>
                rule.Details.Count().ShouldEqual(2);

            It should_have_a_required_detail = () =>
                rule.Details.First().Id.ShouldEqual("Required");

            It should_have_a_cool_detail = () =>
                rule.Details.Last().Id.ShouldEqual("Cool");

            static SyntaxNode tree;

            static Rule[] rules;

            static Rule rule;
        }

        [Subject(typeof(RuleTokenizer))]
        class for_two_rule : with_documenter
        {
            Establish context = () =>
            {
                var singleRule = Syntax.ExpressionStatement(
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Tokens.RuleForName,
                            Syntax.IdentifierName("Required")),
                        Syntax.ArgumentList()));
                tree = Syntax.Block(new StatementSyntax[] { singleRule, singleRule });
            };

            Because of = () =>
            {
                rules = documenter.Get(tree).ToArray();
            };

            It should_return_two_rules = () =>
                rules.Length.ShouldEqual(2);

            It should_get_first_rules_name = () =>
                rules[0].Name.ShouldEqual("Name");

            It should_return_a_single_detail = () =>
                rules[0].Details.Count().ShouldEqual(1);

            It should_have_a_required_detail = () =>
                rules[0].Details.First().Id.ShouldEqual("Required");

            It second_rule_should_return_required = () =>
                rules[1].Name.ShouldEqual("Name");

            It second_rule_should_return_a_single_detail = () =>
                rules[1].Details.Count().ShouldEqual(1);

            It second_rule_should_have_a_required_detail = () =>
                rules[1].Details.First().Id.ShouldEqual("Required");

            static SyntaxNode tree;

            static Rule[] rules;
        }

        class with_documenter
        {
            Establish context = () =>
            {
                var ruleDocumenter = An<INodeTokenizer>();
                var methodDocumenter = An<INodeTokenizer>();

                ruleDocumenter.WhenToldTo(r => r.CanProcess(Param.IsAny<SyntaxNode>()))
                    .Return<SyntaxNode>(m =>
                    {
                        var identifier = m.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
                        return identifier != null && identifier.Identifier.ValueText == "RuleFor";
                    });
                ruleDocumenter.WhenToldTo(r => r.IsNewRule).Return(true);
                ruleDocumenter.WhenToldTo(r => r.Get(Param.IsAny<SyntaxNode>())).Return(new Token("RuleFor", "Name"));

                methodDocumenter.WhenToldTo(r => r.CanProcess(Param.IsAny<SyntaxNode>())).Return<SyntaxNode>(
                    n => n is MemberAccessExpressionSyntax);
                methodDocumenter.WhenToldTo(r => r.Get(Param.IsAny<SyntaxNode>()))
                    .Return<SyntaxNode>(m => new Token(m.ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.ValueText));

                documenter = new RuleTokenizer(new[] { ruleDocumenter, methodDocumenter });
            };

            protected static RuleTokenizer documenter;
        }
    }
}