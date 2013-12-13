using System.Linq;
using System.Text;
using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceDocumenter : IRuleDocumenter
    {
        public string ToString(Rule rule)
        {
            var tokens = rule.Details.ToArray();
            if (tokens.Length == 0)
            {
                return string.Empty;
            }

            var result = new StringBuilder(rule.Name);

            foreach (var token in tokens)
            {
                result.AppendFormat(" is {0}", token.Id);
            }

            return result.ToString();
        }
    }
}