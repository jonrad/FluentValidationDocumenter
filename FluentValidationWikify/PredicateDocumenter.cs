using System;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class PredicateDocumenter : IMethodDocumenter
    {
        public bool CanProcess(MethodDeclarationSyntax method)
        {
            return method.Identifier.ValueText == "Must";
        }

        public string Get(MethodDeclarationSyntax method)
        {
            throw new NotImplementedException();
        }
    }
}