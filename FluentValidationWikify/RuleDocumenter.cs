using System.Collections.Generic;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class RuleDocumenter
    {
        private readonly IEnumerable<IMethodDocumenter> methodDocumenters;

        public RuleDocumenter(IEnumerable<IMethodDocumenter> methodDocumenters)
        {
            this.methodDocumenters = methodDocumenters;
        }

        public IEnumerable<Rule> Get(SyntaxNode tree)
        {
            var methods = tree.DescendantNodes().OfType<MethodDeclarationSyntax>();

            // holy inefficiency batman
            foreach (var method in methods)
            {
                foreach (var documenter in methodDocumenters)
                {
                    if (documenter.IsNewRule && documenter.CanProcess(method))
                    {
                        yield return new Rule
                        {
                            Name = documenter.Get(method)
                        };
                    }
                }
            }
        }
    }
}