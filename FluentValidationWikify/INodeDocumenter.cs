using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface INodeDocumenter
    {
        bool IsNewRule { get; }

        bool CanProcess(SyntaxNode node);

        string Get(SyntaxNode node);
    }
}