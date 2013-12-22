using Castle.Windsor;
using Castle.Windsor.Installer;

namespace FluentValidationWikify.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            var shell = container.Resolve<IShell>();
            shell.Run(args);

            container.Dispose();

            System.Console.ReadLine();
        }
    }
}
