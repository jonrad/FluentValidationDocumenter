using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(RuleDocumenter))]
    public class RuleDocumenterSpecs : WithFakes
    {
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