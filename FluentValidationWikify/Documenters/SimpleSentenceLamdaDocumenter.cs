using System;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceLamdaDocumenter : ILamdaDocumenter
    {
        private readonly string ruleName;

        public SimpleSentenceLamdaDocumenter(string ruleName)
        {
            this.ruleName = ruleName;
        }

        public string Document(SimpleLambdaExpressionSyntax lamdaExpression)
        {
            var parameter = lamdaExpression.ChildNodes().OfType<ParameterSyntax>().First().Identifier.ValueText;
            SyntaxNode body = lamdaExpression.Body;

            var visitor = new ReplacementVisitor(parameter, ruleName);
            var replaced = body.ReplaceNodes(body.DescendantNodes(), (_, n) => visitor.Visit(n));

            return replaced.ToString();
        }

        private SyntaxNode Replace(SyntaxNode syntaxNode)
        {
            return syntaxNode;
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
                if (node.Identifier.ValueText == search)
                {
                    return Syntax.IdentifierName(replace);
                }

                return Syntax.IdentifierName(node.Identifier.ValueText.ToLower());
            }

            public override SyntaxNode Visit(SyntaxNode node)
            {
                return base.Visit(node) ?? node;
            }
        }
    }
}