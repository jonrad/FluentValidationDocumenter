using Castle.Windsor;
using FluentValidationWikify.Console.Installers;
using NDesk.Options;

namespace FluentValidationWikify.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var verbose = 0;
            var options = new OptionSet
            {
                { "v|verbose", v => verbose++ },
            };

            options.Parse(args);

            var container = new WindsorContainer();
            container.Install(
                new AppInstaller(),
                new LoggingInstaller(verbose));

            var shell = container.Resolve<IShell>();
            shell.Run(args);

            container.Dispose();

            System.Console.ReadLine();
        }
    }
}
