using System.Collections.Generic;

namespace FluentValidationWikify.Models
{
    public class Rule
    {
        public string Name { get; set; }

        public IEnumerable<Token> Details { get; set; }
    }
}