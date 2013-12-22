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
            var options = new OptionSet
            {
                { "v|verbose", v => verbose++ },
                { "q|quiet", v => verbose-- },
                { "d|debug", v => Debug() },
            };

            var unhandled = options.Parse(args);

            if (installers == null)
            {
                installers = new IWindsorInstaller[]
                {
                    new AppInstaller(),
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
    }
}
