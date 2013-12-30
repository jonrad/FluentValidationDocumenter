using FluentValidationWikify.NodeTokenizers;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs.NodeTokenizers
{
    public class RuleForTokenizerSpecs
    {
        [Subject(typeof(RuleForTokenizer))]
        public class when_processing
        {
            Establish context = () =>
            {
                // RuleFor(m => m.Name)
                node = Tokens.RuleForName;

                tokenizer = new RuleForTokenizer();
            };

            It should_be_able_to_process = () =>
                tokenizer.CanProcess(node).ShouldBeTrue();

            It should_return_required = () =>
                tokenizer.Get(node).Info.ShouldEqual("Name");

            static INodeTokenizer tokenizer;

            static SyntaxNode node;
        }
    }
}