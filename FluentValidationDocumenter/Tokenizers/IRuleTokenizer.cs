using System.Collections.Generic;
using FluentValidationDocumenter.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Tokenizers
{
    public interface IRuleTokenizer
    {
        IEnumerable<Rule> Get(SyntaxNode tree);
    }
}