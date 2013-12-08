using System.Collections.Generic;

namespace FluentValidationWikify
{
    public class ClassRules : List<string>
    {
        public ClassRules(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}