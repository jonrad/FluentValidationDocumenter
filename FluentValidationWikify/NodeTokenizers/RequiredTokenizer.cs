using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public abstract class RequiredTokenizer : MemberAccessExpressionTokenizer
    {
        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Token Get(SyntaxNode node)
        {
            return new Token("required", null);
        }
    }
}