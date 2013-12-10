using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeDocumenters
{
    public abstract class RequiredDocumenter : MemberAccessExpressionDocumenter
    {
        public override bool IsNewRule
        {
            get { return false; }
        }

        public override string Get(SyntaxNode node)
        {
            return "Required";
        }
    }
}