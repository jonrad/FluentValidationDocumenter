using System;
using System.Linq;
using FluentValidationDocumenter.Models;

namespace FluentValidationDocumenter.Documenters
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

            return "Rules for " + classRules.Name + Environment.NewLine +
                   string.Join(
                       Environment.NewLine,
                       classRules.AsParallel().AsOrdered().Select(r => ruleDocumenter.Document(classRules.Name, r)));
        }
    }
}