using Roslyn.Compilers.CSharp;

namespace FluentValidationDocumenter.Documenters
{
    public interface ILamdaDocumenter
    {
        string Document(string ruleName, SimpleLambdaExpressionSyntax lamdaExpression);
    }
}