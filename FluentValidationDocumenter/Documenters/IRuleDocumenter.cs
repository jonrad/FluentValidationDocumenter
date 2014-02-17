using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public interface IRuleDocumenter
    {
        string Document(string className, Rule rule);
    }
}