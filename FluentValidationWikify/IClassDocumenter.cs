using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface IClassDocumenter
    {
        ClassRules Get(ClassDeclarationSyntax node);
    }
}