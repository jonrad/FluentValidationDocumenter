using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceLamdaDocumenter : ILamdaDocumenter
    {
        private readonly string ruleName;

        public SimpleSentenceLamdaDocumenter(string ruleName)
        {
            this.ruleName = ruleName;
        }

        public string Document(SimpleLambdaExpressionSyntax lamdaExpression)
        {
            throw new System.NotImplementedException();
        }
    }
}