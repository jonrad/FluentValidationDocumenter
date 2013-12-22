using System;
using NDesk.Options;

namespace FluentValidationWikify.Console
{
    public class Shell : IShell
    {
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

            System.Console.WriteLine("Here");
            System.Console.ReadLine();
        }

        private void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}