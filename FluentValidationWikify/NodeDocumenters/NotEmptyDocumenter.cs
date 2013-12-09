using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeDocumenters
{
    public class NotEmptyDocumenter : MemberAccessExpressionDocumenter
    {
        public override bool IsNewRule
        {
            get { return false; }
        }

        public override string MethodName
        {
            get { return "NotNull"; }
        }

        public override string Get(SyntaxNode node)
        {
            return "Required";
        }
    }
}