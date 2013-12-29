using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Tokenizers
{
    public interface IClassTokenizer
    {
        ClassRules Get(ClassDeclarationSyntax node);
    }
}