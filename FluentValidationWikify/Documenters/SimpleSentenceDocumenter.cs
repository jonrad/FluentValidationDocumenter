using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceDocumenter : IRuleDocumenter
    {
        private readonly Dictionary<string, Func<Token, string>> tokenStringifiers;

        public SimpleSentenceDocumenter()
        {
            tokenStringifiers = new Dictionary<string, Func<Token, string>>
            {
                {"required", t => "is required"},
                {"must", MustParser}
            };
        }

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
                Func<Token, string> handler;
                tokenStringifiers.TryGetValue(token.Id, out handler);

                if (handler != null)
                {
                    result.AppendFormat(" " + handler(token));
                }
            }

            return result.ToString();
        }

        private string MustParser(Token token)
        {
            return "must " + Friendly(token.Info as string);
        }

        private string Friendly(string data)
        {
            return new Regex("([a-z])([A-Z])").Replace(data, "$1 $2").ToLower();
        }
    }
}