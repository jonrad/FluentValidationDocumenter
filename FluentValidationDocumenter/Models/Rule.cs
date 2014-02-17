using System.Collections.Generic;

namespace FluentValidationDocumenter.Models
{
    public class Rule
    {
        public Rule()
        {
        }

        public Rule(string name, params Token[] tokens)
        {
            Name = name;
            Details = tokens;
        }

        public string Name { get; set; }

        public IEnumerable<Token> Details { get; set; }
    }
}