using System.Collections.Generic;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class RuleDocumenter : IRuleDocumenter
    {
        private readonly IEnumerable<INodeDocumenter> methodDocumenters;

        public RuleDocumenter(IEnumerable<INodeDocumenter> methodDocumenters)
        {
            this.methodDocumenters = methodDocumenters;
        }

        public IEnumerable<Rule> Get(SyntaxNode tree)
        {
            var methods = tree.DescendantNodes().OfType<MethodDeclarationSyntax>();

            Rule rule = null;
            List<string> details = null;

            // holy inefficiency batman
            foreach (var method in methods)
            {
                foreach (var documenter in methodDocumenters)
                {
                    if (documenter.IsNewRule && documenter.CanProcess(method))
                    {
                        if (rule != null)
                        {
                            yield return rule;
                        }

                        details = new List<string>();
                        rule = new Rule
                        {
                            Name = documenter.Get(method),
                            Details = details
                        };

                        break;
                    }

                    if (rule != null && documenter.CanProcess(method))
                    {
                        details.Add(documenter.Get(method));
                        break;
                    }
                }
            }

            if (rule != null)
            {
                yield return rule;
            }
        }
    }
}