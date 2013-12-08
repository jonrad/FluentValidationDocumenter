using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface IMethodDocumenter
    {
        bool IsNewRule { get; }

        bool CanProcess(MethodDeclarationSyntax method);

        string Get(SyntaxNode node);
    }
}