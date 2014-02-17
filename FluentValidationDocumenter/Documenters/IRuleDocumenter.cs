using FluentValidationDocumenter.Models;

namespace FluentValidationDocumenter.Documenters
{
    public interface IRuleDocumenter
    {
        string Document(string className, Rule rule);
    }
}