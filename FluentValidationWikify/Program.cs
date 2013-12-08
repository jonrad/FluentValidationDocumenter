using System;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var text = @"
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
        }
    }
";
            var tree = SyntaxTree.ParseText("RuleFor(m => m.Name);");
            var printer = new PrintVisitor(new TypeNameVisitor());
            printer.Visit(tree.GetRoot());
            Console.ReadLine();
        }
    }
}
