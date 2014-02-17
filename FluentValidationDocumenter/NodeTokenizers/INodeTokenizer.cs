using FluentValidationDocumenter.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.NodeTokenizers
{
    public interface INodeTokenizer
    {
        bool IsNewRule { get; }

        bool CanProcess(SyntaxNode node);

        Token Get(SyntaxNode node);
    }
}