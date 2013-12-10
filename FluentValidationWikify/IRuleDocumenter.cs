using System.Collections.Generic;
using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface IRuleDocumenter
    {
        IEnumerable<Rule> Get(SyntaxNode tree);
    }
}