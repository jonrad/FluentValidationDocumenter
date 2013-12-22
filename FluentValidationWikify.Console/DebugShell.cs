using System.IO;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Console
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
            System.Console.WriteLine("Press any key");
            System.Console.Read();
        }
    }
}