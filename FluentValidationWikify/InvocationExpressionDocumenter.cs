using System.Linq;
using FluentValidationWikify.NodeDocumenters;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public abstract class InvocationExpressionDocumenter : INodeDocumenter
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

        public abstract string Get(SyntaxNode node);
    }
}