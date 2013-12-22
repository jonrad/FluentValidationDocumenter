using System;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public abstract class SingleArgumentTokenizer : MemberAccessExpressionTokenizer
    {
        public override Token Get(SyntaxNode node)
        {
            var invocation = (InvocationExpressionSyntax)node;
            var argument = invocation
                .ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>().First()
                .ChildNodes().First();

            var literal = argument as LiteralExpressionSyntax;

            if (literal != null)
            {
                return new Token(MethodName.ToLower(), literal.Token.Value);
            }

            throw new NotImplementedException();
        }
    }
}