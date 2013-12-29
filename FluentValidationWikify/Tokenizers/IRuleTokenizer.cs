using System.Collections.Generic;
using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Tokenizers
{
    public interface IRuleTokenizer
    {
        IEnumerable<Rule> Get(SyntaxNode tree);
    }
}