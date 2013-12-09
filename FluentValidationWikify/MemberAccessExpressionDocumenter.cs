using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public abstract class MemberAccessExpressionDocumenter : INodeDocumenter
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

        public abstract string Get(SyntaxNode node);
    }
}