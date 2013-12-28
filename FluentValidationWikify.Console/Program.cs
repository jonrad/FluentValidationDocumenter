using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentValidationWikify.Console.Installers;
using NDesk.Options;

namespace FluentValidationWikify.Console
{
    public class Program
    {
        private static IWindsorInstaller[] installers;

        public static void Main(string[] args)
        {
            var verbose = 1;
            bool force = true;

            var options = new OptionSet
            {
                { "v|verbose", v => verbose++ },
                { "q|quiet", v => verbose-- },
                { "d|debug", v => Debug() },
                { "f|force", v => force = true },
                { "h|help|?", v => ShowHelp() }
            };

            var unhandled = options.Parse(args);

            if (installers == null)
            {
                if (unhandled.Count == 0)
                {
                    ShowHelp();
                }

                installers = new IWindsorInstaller[]
                {
                    new AppInstaller(force),
                    new LoggingInstaller(verbose)
                };
            }

            var container = new WindsorContainer();
            container.Install(installers);

            var shell = container.Resolve<IShell>();
            shell.Run(unhandled.ToArray());

            container.Dispose();

            System.Console.ReadLine();
        }

        public static void Debug()
        {
            installers = new IWindsorInstaller[]
            {
                new DebugInstaller()
            };
        }

        private static void ShowHelp()
        {
            throw new NotImplementedException();
        }
    }
}
