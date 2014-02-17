using System.Collections.Generic;

namespace FluentValidationDocumenter.Models
{
    public class ClassRules : List<Rule>
    {
        public ClassRules(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}