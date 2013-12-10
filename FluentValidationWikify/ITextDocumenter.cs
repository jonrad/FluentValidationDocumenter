using System.Collections.Generic;
using FluentValidationWikify.Models;

namespace FluentValidationWikify
{
    public interface ITextDocumenter
    {
        IEnumerable<ClassRules> Get(string text);
    }
}