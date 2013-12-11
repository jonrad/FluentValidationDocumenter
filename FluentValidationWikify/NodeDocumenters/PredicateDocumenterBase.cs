using System;
using System.Linq;
using System.Text.RegularExpressions;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeDocumenters
{
    public abstract class PredicateDocumenterBase : MemberAccessExpressionDocumenter
    {
        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Doc Get(SyntaxNode node)
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

            return new Doc(MethodName, valueText);
        }
    }
}