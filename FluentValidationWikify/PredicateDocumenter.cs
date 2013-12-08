using System;
using System.Linq;
using System.Text.RegularExpressions;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class PredicateDocumenter : IMethodDocumenter
    {
        public bool IsNewRule
        {
            get { return false; }
        }

        public bool CanProcess(MethodDeclarationSyntax method)
        {
            return method.Identifier.ValueText == "Must";
        }

        public string Get(SyntaxNode node)
        {
            var valueText = node
                .ChildNodes().OfType<ParameterListSyntax>().First()
                .ChildNodes().OfType<ParameterSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().First()
                .Identifier.ValueText;

            if (valueText == null)
            {
                throw new NotImplementedException();
            }

            valueText = Regex.Replace(valueText, "([a-z])([A-Z])", "$1 $2");
            return "Must " + valueText;
        }
    }
}