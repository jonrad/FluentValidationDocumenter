using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class Tokens
    {
        public static InvocationExpressionSyntax RuleForName = 
            Syntax.InvocationExpression(
                Syntax.IdentifierName("RuleFor"),
                Syntax.ArgumentList(
                    Syntax.SeparatedList(
                        Syntax.Argument(
                            Syntax.SimpleLambdaExpression(
                                Syntax.Parameter(Syntax.Identifier("m")),
                                Syntax.MemberAccessExpression(
                                    SyntaxKind.MemberAccessExpression,
                                    Syntax.IdentifierName("m"),
                                    Syntax.IdentifierName("Name")))))));
    }
}