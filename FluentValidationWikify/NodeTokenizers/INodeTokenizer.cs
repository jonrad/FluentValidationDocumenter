using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public interface INodeTokenizer
    {
        bool IsNewRule { get; }

        bool CanProcess(SyntaxNode node);

        Token Get(SyntaxNode node);
    }
}