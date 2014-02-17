using FluentValidationDocumenter.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Tokenizers
{
    public interface IClassTokenizer
    {
        ClassRules Get(ClassDeclarationSyntax node);
    }
}