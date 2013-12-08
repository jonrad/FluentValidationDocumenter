using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class ClassDocumenter
    {
        private readonly IRuleDocumenter ruleDocumenter;

        public ClassDocumenter(IRuleDocumenter ruleDocumenter)
        {
            this.ruleDocumenter = ruleDocumenter;
        }

        public bool CanProcess(ClassDeclarationSyntax node)
        {
            return true;
        }

        public ClassRules Get(ClassDeclarationSyntax node)
        {
            var name = node
                .ChildNodes().OfType<BaseListSyntax>().First()
                .ChildNodes().OfType<GenericNameSyntax>().First()
                .ChildNodes().OfType<TypeArgumentListSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.ValueText;

            return new ClassRules(name);
        }
    }
}