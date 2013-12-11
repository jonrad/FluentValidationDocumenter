using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public abstract class MemberAccessExpressionTokenizer : INodeTokenizer
    {
        public abstract string MethodName { get; }

        public abstract bool IsNewRule { get; }

        public bool CanProcess(SyntaxNode node)
        {
            var invocation = node as InvocationExpressionSyntax;
            if (invocation == null)
            {
                return false;
            }

            var memberAccess = invocation.ChildNodes().OfType<MemberAccessExpressionSyntax>().FirstOrDefault();
            if (memberAccess == null)
            {
                return false;
            }

            var identifier = memberAccess.ChildNodes().OfType<IdentifierNameSyntax>().LastOrDefault();
            return identifier != null && identifier.Identifier.ValueText == MethodName;
        }

        public abstract Token Get(SyntaxNode node);
    }
}