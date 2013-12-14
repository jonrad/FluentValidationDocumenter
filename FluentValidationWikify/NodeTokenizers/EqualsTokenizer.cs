using System;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public class EqualsTokenizer : MemberAccessExpressionTokenizer
    {
        public override string MethodName
        {
            get { return "Equals"; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }

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
                return new Token("equals", literal.Token.Value);
            }

            throw new NotImplementedException();
        }
    }
}