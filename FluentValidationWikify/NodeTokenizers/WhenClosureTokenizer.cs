using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
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

            var details = new WhenClosureDetails(args.First().ChildNodes().First(), null);

            return new Token("WhenClosure", details);
        }
    }
}