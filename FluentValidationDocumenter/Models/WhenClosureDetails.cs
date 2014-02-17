using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Models
{
    public class WhenClosureDetails
    {
        public SyntaxNode WhenDetails { get; set; }

        public SyntaxNode Block { get; set; }

        public WhenClosureDetails(SyntaxNode whenDetails, SyntaxNode block)
        {
            WhenDetails = whenDetails;
            Block = block;
        }
    }
}