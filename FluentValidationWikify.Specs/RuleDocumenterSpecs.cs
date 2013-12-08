using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(RuleDocumenter))]
    public class RuleDocumenterSpecs
    {
        Establish context = () =>
        {
            tree = SyntaxTree.ParseText("RuleFor(m => m.Name).NotEmpty()").GetRoot();

            documenter = new RuleDocumenter();
        };

        Because of =
            () => rule = documenter.Get(tree);

        It should_return_required =
            () => rule.Name.ShouldEqual("Name");

        static RuleDocumenter documenter;

        static SyntaxNode tree;

        static Rule rule;
    }
}