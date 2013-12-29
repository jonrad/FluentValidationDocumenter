using System.Linq;
using FluentValidationWikify.Models;
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

            var tokenDetails = GetDetails(argument);

            return new Token(Identifier(node).ToLower(), tokenDetails);
        }

        private object GetDetails(ArgumentSyntax argument)
        {
            var identifier = argument.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault();
            if (identifier == null)
            {
                // this is really quite crap
                SyntaxNode child = argument.ChildNodes().OfType<IdentifierNameSyntax>().FirstOrDefault() ??
                                   (SyntaxNode)argument.ChildNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault();

                if (child != null)
                {
                    identifier = child.ChildNodes().OfType<IdentifierNameSyntax>().First();
                }
            }

            if (identifier != null)
            {
                return identifier.Identifier.ValueText;
            }

            var lamda = argument.ChildNodes().OfType<SimpleLambdaExpressionSyntax>().FirstOrDefault();

            return lamda;
        }
    }
}