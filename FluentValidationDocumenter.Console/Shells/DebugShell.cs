using System.IO;
using FluentValidationWikify.Visitors;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Console.Shells
{
    public class DebugShell : IShell
    {
        public void Run(params string[] args)
        {
            foreach (var file in args)
            {
                var text = File.ReadAllText(file);
                var tree = SyntaxTree.ParseText(text);
                var printer = new PrintVisitor(new TypeNameVisitor());
                printer.Visit(tree.GetRoot());
            }

            System.Console.WriteLine();
        }
    }
}