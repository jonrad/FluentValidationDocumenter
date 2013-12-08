using System.Collections.Generic;

namespace FluentValidationWikify
{
    public class Rule
    {
        public string Name { get; set; }

        public IEnumerable<string> Details { get; set; }
    }
}