using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class NotEmptyDocumenter : IMethodDocumenter
    {
        public bool IsNewRule
        {
            get { return false; }
        }

        public bool CanProcess(MethodDeclarationSyntax method)
        {
            return method.Identifier.ValueText == "NotNull";
        }

        public string Get(MethodDeclarationSyntax method)
        {
            return "Required";
        }
    }
}