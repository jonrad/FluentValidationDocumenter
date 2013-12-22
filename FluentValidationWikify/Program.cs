using System;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string Text = @"
    public class ModelValidator : AbstractValidator<Model>
    {
        public ModelValidator()
        {
            RuleFor(t => t.Name)
                .Must(Exist(t => new Person(t.Name)));
        }
    }
";
            var tree = SyntaxTree.ParseText(Text);
            var printer = new PrintVisitor(new TypeNameVisitor());
            printer.Visit(tree.GetRoot());
            Console.ReadLine();
        }
    }
}
