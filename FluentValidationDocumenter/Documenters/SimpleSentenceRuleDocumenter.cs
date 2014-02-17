using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceRuleDocumenter : IRuleDocumenter
    {
        private readonly ILamdaDocumenter lamdaDocumenter;

        private readonly IFriendly friendly;

        private readonly Dictionary<string, Func<string, Rule, Token, string>> tokenStringifiers;

        public SimpleSentenceRuleDocumenter(ILamdaDocumenter lamdaDocumenter, IFriendly friendly)
        {
            this.lamdaDocumenter = lamdaDocumenter;
            this.friendly = friendly;

            tokenStringifiers = new Dictionary<string, Func<string, Rule, Token, string>>
            {
                { "notnull", (unused, unused2, t) => "is required" },
                { "notempty", (unused, unused2, t) => "must not be empty" },
                { "required", (unused, unused2, t) => "is required" },
                { "must", MustParser },
                { "when", WhenParser },
                { "equal", (unused, unused2, t) => ArgumentParser(t, "equal") },
                { "notequal", (unused, unused2, t) => ArgumentParser(t, "not equal") },
                { "greaterthan", (unused, unused2, t) => ArgumentParser(t, "be greater than") },
                { "greaterthanorequalto", (unused, unused2, t) => ArgumentParser(t, "be greater than or equal to") },
                { "inclusivebetween", (unused, unused2, t) => BetweenParser(t, "inclusive") },
                { "exclusivebetween", (unused, unused2, t) => BetweenParser(t, "exclusive") },
                { "length", (unused, unused2, t) => LengthParser(t) },
            };
        }

        public string Document(string className, Rule rule)
        {
            var tokens = rule.Details.ToArray();
            if (tokens.Length == 0)
            {
                return string.Empty;
            }

            var ruleDetails = string.Join(" and ", tokens.Where(w => w.Id != "when").Select(t => HandleToken(className, rule, t)));
            var conditionalDetails = string.Join(" and ", tokens.Where(w => w.Id == "when").Select(t => HandleToken(className, rule, t)));
            return rule.Name + " " +
                   ruleDetails +
                   (ruleDetails != string.Empty && conditionalDetails != string.Empty ? " " : string.Empty) +
                   conditionalDetails;
        }

        private string HandleToken(string className, Rule rule, Token token)
        {
            Func<string, Rule, Token, string> handler;
            tokenStringifiers.TryGetValue(token.Id, out handler);

            return handler != null ? handler(className, rule, token) : null;
        }

        private string MustParser(string className, Rule rule, Token token)
        {
            var info = token.Info as SimpleLambdaExpressionSyntax;

            var details = 
                info != null ?
                "satisfy " + lamdaDocumenter.Document(friendly.Get(rule.Name), info) :
                friendly.Get(token.Info.ToString());
            return "must " + details;
        }

        private string WhenParser(string className, Rule rule, Token token)
        {
            var info = token.Info as SimpleLambdaExpressionSyntax;
            var details = info != null ? lamdaDocumenter.Document(friendly.Get(className), info) : friendly.Get(token.Info.ToString());
            return "when " + details;
        }

        private string ArgumentParser(Token token, string text)
        {
            return string.Concat("must ", text, " ", token.Info);
        }

        private string LengthParser(Token token)
        {
            var args = (object[])token.Info;
            return string.Format("must have length between {0} and {1}", args[0], args[1]);
        }

        private string BetweenParser(Token token, string type)
        {
            var args = (object[])token.Info;
            return string.Format("must be between {0} and {1} ({2})", args[0], args[1], type);
        }
    }
}
