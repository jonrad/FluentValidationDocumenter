using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class TypeNameVisitor : SyntaxVisitor<string>
    {
        public override string Visit(SyntaxNode node)
        {
            return node.GetType().Name;
        }
    }
}