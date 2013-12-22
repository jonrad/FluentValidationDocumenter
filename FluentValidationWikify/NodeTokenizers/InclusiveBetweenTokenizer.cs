using System;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeTokenizers
{
    public class InclusiveBetweenTokenizer : MemberAccessExpressionTokenizer
    {
        public override string MethodName
        {
            get { return "InclusiveBetween"; }
        }

        public override bool IsNewRule
        {
            get { return false; }
        }

        public override Token Get(SyntaxNode node)
        {
            var invocation = (InvocationExpressionSyntax)node;
            var arguments = invocation
                .ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>()
                .SelectMany(a => a.ChildNodes().OfType<LiteralExpressionSyntax>())
                .Select(t => t.Token.Value)
                .ToArray();

            return new Token(MethodName.ToLower(), arguments);
        }
    }
}