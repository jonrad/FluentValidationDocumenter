using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(RuleDocumenter))]
    public class RuleDocumenterSpecs : WithFakes
    {
        Establish context = () =>
        {
            tree = SyntaxTree.ParseText("RuleFor(m => m.Name).NotEmpty()").GetRoot();

            var methods = tree.DescendantNodes().OfType<MethodDeclarationSyntax>().ToArray();
            var ruleMethod = methods[0];
            var notEmptyMethod = methods[1];

            var ruleDocumenter = An<IMethodDocumenter>();
            var methodDocumenter = An<IMethodDocumenter>();

            ruleDocumenter.WhenToldTo(r => r.CanProcess(ruleMethod)).Return(true);
            ruleDocumenter.WhenToldTo(r => r.IsNewRule).Return(true);
            ruleDocumenter.WhenToldTo(r => r.Get(ruleMethod)).Return("Name");

            methodDocumenter.WhenToldTo(r => r.CanProcess(notEmptyMethod)).Return(true);
            methodDocumenter.WhenToldTo(r => r.Get(notEmptyMethod)).Return("Required");

            documenter = new RuleDocumenter(new[] { ruleDocumenter, methodDocumenter });
        };

        Because of = () =>
            rule = documenter.Get(tree);

        It should_return_required = () =>
            rule.Name.ShouldEqual("Name");

        static RuleDocumenter documenter;

        static SyntaxNode tree;

        static Rule rule;
    }
}