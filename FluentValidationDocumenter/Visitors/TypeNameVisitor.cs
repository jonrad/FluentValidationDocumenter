using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Visitors
{
    public class TypeNameVisitor : SyntaxVisitor<string>
    {
        public override string Visit(SyntaxNode node)
        {
            return node.GetType().Name + " " + node.GetText().Lines.First().ToString().Trim();
        }
    }
}