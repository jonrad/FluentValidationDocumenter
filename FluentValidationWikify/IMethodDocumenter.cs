using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface IMethodDocumenter
    {
        bool CanProcess(MethodDeclarationSyntax method);

        string Get(MethodDeclarationSyntax method);
    }
}