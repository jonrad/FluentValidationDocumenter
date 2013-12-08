using System.Collections.Generic;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public interface IRuleDocumenter
    {
        IEnumerable<Rule> Get(SyntaxNode tree);
    }
}