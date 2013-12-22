﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceRuleDocumenter : IRuleDocumenter
    {
        private readonly Dictionary<string, Func<Token, string>> tokenStringifiers;

        public SimpleSentenceRuleDocumenter()
        {
            tokenStringifiers = new Dictionary<string, Func<Token, string>>
            {
                { "required", t => "is required" },
                { "must", MustParser },
                { "when", WhenParser },
                { "equal", t => ArgumentParser(t, "equal") },
                { "greaterthan", t => ArgumentParser(t, "greater than") },
                { "greaterthanorequalto", t => ArgumentParser(t, "greater than or equal to") },
                { "inclusivebetween", t => BetweenParser(t, "inclusive") },
            };
        }

        public string ToString(Rule rule)
        {
            var tokens = rule.Details.ToArray();
            if (tokens.Length == 0)
            {
                return string.Empty;
            }

            var ruleDetails = string.Join(" and ", tokens.Where(w => w.Id != "when").Select(HandleToken));
            var conditionalDetails = string.Join(" and ", tokens.Where(w => w.Id == "when").Select(HandleToken));
            return rule.Name + " " +
                   ruleDetails +
                   (ruleDetails != string.Empty && conditionalDetails != string.Empty ? " " : string.Empty) +
                   conditionalDetails;
        }

        private string HandleToken(Token token)
        {
            Func<Token, string> handler;
            tokenStringifiers.TryGetValue(token.Id, out handler);

            if (handler != null)
            {
                return handler(token);
            }

            return null;
        }

        private string MustParser(Token token)
        {
            return "must " + Friendly(token.Info as string);
        }

        private string WhenParser(Token token)
        {
            return "when " + Friendly(token.Info as string);
        }

        private string ArgumentParser(Token token, string text)
        {
            return string.Concat("must ", text, " ", token.Info);
        }

        private string BetweenParser(Token token, string type)
        {
            var args = (object[])token.Info;
            return string.Format("must be between {0} and {1} ({2})", args[0], args[1], type);
        }

        private string Friendly(string data)
        {
            return new Regex("([a-z])([A-Z])").Replace(data, "$1 $2").ToLower();
        }
    }
}