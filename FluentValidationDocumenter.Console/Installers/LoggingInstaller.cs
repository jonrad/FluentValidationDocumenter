﻿using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace FluentValidationDocumenter.Console.Installers
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
            if (verbose <= 0)
            {
                return;
            }

            var level = verbose >= 2 ? LogLevel.Debug : LogLevel.Warn;

            var config = new LoggingConfiguration();
            var target = new ConsoleTarget
            {
                Layout = @"${message}${onexception:${newline}EXCEPTION OCCURRED\: ${exception:format=tostring}}"
            };

            var rule = new LoggingRule("*", level, target);
            config.LoggingRules.Add(rule);
            config.AddTarget("console", target);
            LogManager.Configuration = config;
        }
    }
}