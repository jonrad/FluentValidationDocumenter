using System.Collections.Generic;
using FluentValidationDocumenter.Models;

namespace FluentValidationDocumenter.Tokenizers
{
    public interface ITextTokenizer
    {
        IEnumerable<ClassRules> Get(string text);
    }
}