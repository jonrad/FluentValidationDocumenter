using System;
using System.Linq;
using FluentValidationWikify.Models;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceClassDocumenter : IClassDocumenter
    {
        private readonly IRuleDocumenter ruleDocumenter;

        public SimpleSentenceClassDocumenter(IRuleDocumenter ruleDocumenter)
        {
            this.ruleDocumenter = ruleDocumenter;
        }

        public string ToString(ClassRules classRules)
        {
            if (!classRules.Any())
            {
                return string.Empty;
            }
            Console.WriteLine("CLASS");

            return "Rules for " + classRules.Name + Environment.NewLine +
                   string.Join(
                       Environment.NewLine,
                       classRules.AsParallel().AsOrdered().Select(r => ruleDocumenter.Document(classRules.Name, r)));
        }
    }
}