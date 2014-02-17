using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public interface IClassDocumenter
    {
        string ToString(ClassRules classRules);
    }
}