using System.IO;
using FluentValidationWikify.Documenters;

namespace FluentValidationWikify.Console
{
    public class Shell : IShell
    {
        private readonly ITextDocumenter documenter;

        public Shell(ITextDocumenter documenter)
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