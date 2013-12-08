using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class ClassDocumenter
    {
        public bool CanProcess(ClassDeclarationSyntax node)
        {
            return true;
        }

        public string Get(ClassDeclarationSyntax node)
        {
            return node
                .ChildNodes().OfType<BaseListSyntax>().First()
                .ChildNodes().OfType<GenericNameSyntax>().First()
                .ChildNodes().OfType<TypeArgumentListSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.ValueText;
        }
    }
}