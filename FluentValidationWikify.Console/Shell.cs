using System;
using System.IO;
using FluentValidationWikify.Documenters;
using NDesk.Options;

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
            var verbose = 0;
            var options = new OptionSet
            {
                { "v|verbose", v => verbose++ },
                { "h|help|?", v => ShowHelp() }
            };

            var files = options.Parse(args);
            if (files.Count == 0)
            {
                ShowHelp();
            }

            foreach (var file in files)
            {
                var text = File.ReadAllText(file);
                System.Console.WriteLine(documenter.ToString(text));
            }
        }

        private void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}