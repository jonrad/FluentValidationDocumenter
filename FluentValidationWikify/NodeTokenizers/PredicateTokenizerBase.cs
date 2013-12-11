using System;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public abstract class PredicateTokenizerBase : MemberAccessExpressionTokenizer
    {
        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Token Get(SyntaxNode node)
        {
            var valueText = node
                .ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().First()
                .Identifier.ValueText;

            if (valueText == null)
            {
                throw new NotImplementedException();
            }

            return new Token(MethodName, valueText);
        }
    }
}