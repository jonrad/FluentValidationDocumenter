using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public interface IRuleDocumenter
    {
        string ToString(Rule rule);
    }
}