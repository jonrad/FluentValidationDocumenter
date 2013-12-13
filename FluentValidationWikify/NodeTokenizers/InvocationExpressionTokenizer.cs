using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public abstract class InvocationExpressionTokenizer : INodeTokenizer
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

            var identifier = invocation.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
            return identifier != null && identifier.Identifier.ValueText == MethodName;
        }

        public abstract Token Get(SyntaxNode node);
    }
}