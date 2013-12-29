using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public class RequiredTokenizer : MemberAccessExpressionTokenizer
    {
        public override string[] MethodNames
        {
            get
            {
                return new[] { "NotNull", "NotEmpty" };
            }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Token Get(SyntaxNode node)
        {
            return new Token(Identifier(node).ToLower(), null);
        }
    }
}