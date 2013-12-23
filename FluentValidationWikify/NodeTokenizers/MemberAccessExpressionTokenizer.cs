using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public abstract class MemberAccessExpressionTokenizer : INodeTokenizer
    {
        public abstract string[] MethodNames { get; }

        public abstract bool IsNewRule { get; }

        public bool CanProcess(SyntaxNode node)
        {
            var identifier = Identifier(node);
            return identifier != null && MethodNames.Contains(identifier);
        }

        public string Identifier(SyntaxNode node)
        {
            var invocation = node as InvocationExpressionSyntax;
            if (invocation == null)
            {
                return null;
            }

            var memberAccess = invocation.ChildNodes().OfType<MemberAccessExpressionSyntax>().FirstOrDefault();
            if (memberAccess == null)
            {
                return null;
            }

            var identifier = memberAccess.ChildNodes().OfType<IdentifierNameSyntax>().LastOrDefault();
            return identifier == null ? null : identifier.Identifier.ValueText;
        }

        public abstract Token Get(SyntaxNode node);
    }
}