using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Documenters
{
    public class SimpleSentenceLamdaDocumenter : ILamdaDocumenter
    {
        public string Document(string ruleName, SimpleLambdaExpressionSyntax lamdaExpression)
        {
            var parameter = lamdaExpression.ChildNodes().OfType<ParameterSyntax>().First().Identifier.ValueText;
            SyntaxNode body = lamdaExpression.Body;

            var visitor = new ReplacementVisitor(parameter, ruleName);
            var replaced = body.ReplaceNodes(body.DescendantNodes(), (_, n) => visitor.Visit(n));

            return replaced.ToString();
        }

        private class ReplacementVisitor : SyntaxVisitor<SyntaxNode>
        {
            private readonly string search;

            private readonly string replace;

            public ReplacementVisitor(string search, string replace)
            {
                this.search = search;
                this.replace = replace;
            }

            public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
            {
                var newIdentifier =
                    node.Identifier.ValueText == search ? replace : node.Identifier.ValueText.ToLower();

                return Syntax.IdentifierName(newIdentifier)
                        .WithLeadingTrivia(node.GetLeadingTrivia())
                        .WithTrailingTrivia(node.GetTrailingTrivia());
            }

            public override SyntaxNode Visit(SyntaxNode node)
            {
                return base.Visit(node) ?? node;
            }
        }
    }
}