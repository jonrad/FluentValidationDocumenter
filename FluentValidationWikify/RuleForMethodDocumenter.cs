using System;
using System.Linq;
using System.Text.RegularExpressions;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class RuleForMethodDocumenter : IMethodDocumenter
    {
        public bool IsNewRule
        {
            get { return true; }
        }

        public bool CanProcess(MethodDeclarationSyntax method)
        {
            return method.Identifier.ValueText == "RuleFor";
        }

        public string Get(MethodDeclarationSyntax method)
        {
            var valueText = method
                .ChildNodes().OfType<ParameterListSyntax>().First()
                .ChildNodes().OfType<ParameterSyntax>().Last()
                .ChildNodes().OfType<QualifiedNameSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().Last()
                .Identifier.ValueText;

            if (valueText == null)
            {
                throw new NotImplementedException();
            }

            valueText = Regex.Replace(valueText, "([a-z])([A-Z])", "$1 $2");
            return valueText;
        }
    }
}