using System.Collections.Generic;
using FluentValidationWikify.Models;

namespace FluentValidationWikify
{
    public interface ITextTokenizer
    {
        IEnumerable<ClassRules> Get(string text);
    }
}