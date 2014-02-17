using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Documenters
{
    public interface ILamdaDocumenter
    {
        string Document(string ruleName, SimpleLambdaExpressionSyntax lamdaExpression);
    }
}