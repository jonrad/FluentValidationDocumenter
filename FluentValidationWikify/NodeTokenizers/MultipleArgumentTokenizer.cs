using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public class MultipleArgumentTokenizer : MemberAccessExpressionTokenizer
    {
        public override string[] MethodNames
        {
            get { return new[] { "InclusiveBetween", "ExclusiveBetween", "Length" }; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Token Get(SyntaxNode node)
        {
            var invocation = (InvocationExpressionSyntax)node;
            var arguments = invocation
                .ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>()
                .SelectMany(a => a.ChildNodes().OfType<LiteralExpressionSyntax>())
                .Select(t => t.Token.Value)
                .ToArray();

            return new Token(Identifier(node).ToLower(), arguments);
        }
    }
}
