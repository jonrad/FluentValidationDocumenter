using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace FluentValidationWikify.Console.Installers
{
    public class LoggingInstaller : IWindsorInstaller
    {
        private readonly int verbose;

        public LoggingInstaller(int verbose)
        {
            this.verbose = verbose;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Configure();
            container.AddFacility<LoggingFacility>(f => f.UseNLog().ConfiguredExternally());
        }

        private void Configure()
        {
            if (verbose == 0)
            {
                return;
            }

            var config = new LoggingConfiguration();
            var target = new ConsoleTarget();
            target.Layout = "${message}";
            var rule = new LoggingRule("*", LogLevel.Debug, target);
            config.LoggingRules.Add(rule);
            config.AddTarget("console", target);
            LogManager.Configuration = config;
        }
    }
}