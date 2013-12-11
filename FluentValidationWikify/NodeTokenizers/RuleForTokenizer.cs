using System;
using System.Linq;
using System.Text.RegularExpressions;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public class RuleForTokenizer : InvocationExpressionTokenizer
    {
        public override string MethodName
        {
            get { return "RuleFor"; }
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

            valueText = Regex.Replace(valueText, "([a-z])([A-Z])", "$1 $2");
            return new Token("RuleFor", valueText);
        }
    }
}