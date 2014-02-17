using FluentValidationDocumenter.Models;

namespace FluentValidationDocumenter.Documenters
{
    public interface IClassDocumenter
    {
        string ToString(ClassRules classRules);
    }
}