using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface IClassTokenizer
    {
        ClassRules Get(ClassDeclarationSyntax node);
    }
}