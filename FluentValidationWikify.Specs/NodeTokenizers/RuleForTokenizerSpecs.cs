using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    [Subject(typeof(RuleForTokenizer))]
    public class RuleForTokenizerSpecs
    {
        Establish context = () =>
        {
            // RuleFor(m => m.Name)
            node = Tokens.RuleForName;

            documenter = new RuleForTokenizer();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).Info.ShouldEqual("Name");

        static INodeTokenizer documenter;

        static SyntaxNode node;
    }
}