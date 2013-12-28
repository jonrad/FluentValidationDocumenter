using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class Tokens
    {
        // RuleFor(m => n.Name)
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

        // p => p.Age < 25
        public static SimpleLambdaExpressionSyntax AgeLessThan25 = Syntax.SimpleLambdaExpression(
            Syntax.Parameter(Syntax.Identifier("p")),
            Syntax.BinaryExpression(
                SyntaxKind.LessThanExpression,
                Syntax.MemberAccessExpression(
                    SyntaxKind.MemberAccessExpression,
                    Syntax.IdentifierName("p"),
                    Syntax.IdentifierName("Age")),
                Syntax.LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    Syntax.Literal("25", 25))));
    }
}