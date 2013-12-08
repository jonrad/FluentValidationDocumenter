using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class RuleDocumenterSpecs : WithFakes
    {
        [Subject(typeof(RuleDocumenter))]
        class for_single_rule : with_documenter
        {
            Establish context = () =>
            {
                tree = SyntaxTree.ParseText("RuleFor(m => m.Name).NotEmpty()").GetRoot();
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
                rule.Details.First().ShouldEqual("Required");

            static SyntaxNode tree;

            static Rule[] rules;

            static Rule rule;
        }

        [Subject(typeof(RuleDocumenter))]
        class for_two_rule : with_documenter
        {
            Establish context = () =>
            {
                tree = SyntaxTree.ParseText("RuleFor(m => m.Name).NotEmpty();RuleFor(m => m.Name).NotEmpty();").GetRoot();
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
                rules[0].Details.First().ShouldEqual("Required");

            It second_rule_should_return_required = () =>
                rules[1].Name.ShouldEqual("Name");

            It second_rule_should_return_a_single_detail = () =>
                rules[1].Details.Count().ShouldEqual(1);

            It second_rule_should_have_a_required_detail = () =>
                rules[1].Details.First().ShouldEqual("Required");

            static SyntaxNode tree;

            static Rule[] rules;
        }

        class with_documenter
        {
            Establish context = () =>
            {
                var ruleDocumenter = An<IMethodDocumenter>();
                var methodDocumenter = An<IMethodDocumenter>();

                ruleDocumenter.WhenToldTo(r => r.CanProcess(Param.IsAny<MethodDeclarationSyntax>()))
                    .Return<MethodDeclarationSyntax>(m => m.Identifier.ValueText == "RuleFor");
                ruleDocumenter.WhenToldTo(r => r.IsNewRule).Return(true);
                ruleDocumenter.WhenToldTo(r => r.Get(Param.IsAny<MethodDeclarationSyntax>())).Return("Name");

                methodDocumenter.WhenToldTo(r => r.CanProcess(Param.IsAny<MethodDeclarationSyntax>())).Return(true);
                methodDocumenter.WhenToldTo(r => r.Get(Param.IsAny<MethodDeclarationSyntax>())).Return("Required");

                documenter = new RuleDocumenter(new[] { ruleDocumenter, methodDocumenter });
            };

            protected static RuleDocumenter documenter;

            static SyntaxNode tree;

            static Rule[] rules;

            static Rule rule;
        }
    }
}