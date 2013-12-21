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
            RuleFor(t => t.Name)
                .Must(Exist(t => new Person(t.Name)));
        }
    }
";
            var tree = SyntaxTree.ParseText(text);
            var printer = new PrintVisitor(new TypeNameVisitor());
            printer.Visit(tree.GetRoot());
            Console.ReadLine();
        }
    }
}
