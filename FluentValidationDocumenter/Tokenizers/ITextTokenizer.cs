using System.Collections.Generic;
using FluentValidationWikify.Models;

namespace FluentValidationWikify.Tokenizers
{
    public interface ITextTokenizer
    {
        IEnumerable<ClassRules> Get(string text);
    }
}