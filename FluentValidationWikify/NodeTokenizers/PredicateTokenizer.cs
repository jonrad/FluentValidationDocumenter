using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public class PredicateTokenizer : MemberAccessExpressionTokenizer
    {
        public override string[] MethodNames
        {
            get { return new[] { "Must", "When" }; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Token Get(SyntaxNode node)
        {
            var argument = node
                .ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>().First();

            var identifier = argument.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
            if (identifier == null)
            {
                // this is really quite crap
                SyntaxNode child = argument.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault() ??
                                   (SyntaxNode)argument.ChildNodes().OfType<InvocationExpressionSyntax>().First();

                identifier = child.ChildNodes().OfType<IdentifierNameSyntax>().First();
            }

            return new Token(Identifier(node).ToLower(), identifier.Identifier.ValueText);
        }
    }
}