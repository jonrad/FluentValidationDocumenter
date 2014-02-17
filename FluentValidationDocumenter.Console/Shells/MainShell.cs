using System.IO;
using FluentValidationWikify.Documenters;

namespace FluentValidationWikify.Console.Shells
{
    public class MainShell : IShell
    {
        private readonly ITextDocumenter documenter;

        public MainShell(ITextDocumenter documenter)
        {
            this.documenter = documenter;
        }

        public void Run(params string[] args)
        {
            var files = args;

            foreach (var file in files)
            {
                var text = File.ReadAllText(file);
                System.Console.WriteLine(documenter.ToString(text));
            }
        }
    }
}