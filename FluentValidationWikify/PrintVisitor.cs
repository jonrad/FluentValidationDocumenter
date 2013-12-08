using System;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class PrintVisitor : SyntaxVisitor
    {
        private readonly SyntaxVisitor<string> textVisitor;

        public PrintVisitor(SyntaxVisitor<string> textVisitor)
        {
            this.textVisitor = textVisitor;
        }

        public override void Visit(SyntaxNode node)
        {
            Visit(node, 0);
        }

        private void Visit(SyntaxNode node, int spaces)
        {
            Console.WriteLine(new string(' ', spaces) + textVisitor.Visit(node));
            foreach (var child in node.ChildNodes())
            {
                Visit(child, spaces + 2);
            }
        }
    }
}