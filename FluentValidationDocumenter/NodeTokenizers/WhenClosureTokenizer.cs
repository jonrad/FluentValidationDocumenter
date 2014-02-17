using System;
using System.Linq;
using FluentValidationDocumenter.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.NodeTokenizers
{
    public class WhenClosureTokenizer : InvocationExpressionTokenizer
    {
        public override string MethodName
        {
            get { return "When"; }
        }

        public override bool IsNewRule
        {
            get { return true; }
        }

        public override Token Get(SyntaxNode node)
        {
            var valueText = node
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Last()
                .Identifier.ValueText;

            if (valueText == null)
            {
                throw new NotImplementedException();
            }

            var args = node.ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>().ToArray();

            var whenClause = args[0].ChildNodes().First();

            var block = args[1]
                .ChildNodes().OfType<ParenthesizedLambdaExpressionSyntax>().First()
                .ChildNodes().OfType<BlockSyntax>().First();

            var details = new WhenClosureDetails(whenClause, block);

            return new Token("WhenClosure", details);
        }
    }
}