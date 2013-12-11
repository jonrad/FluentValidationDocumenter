using System.Collections.Generic;

namespace FluentValidationWikify.Models
{
    public class Rule
    {
        public string Name { get; set; }

        public IEnumerable<Doc> Details { get; set; }
    }
}