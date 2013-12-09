using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace FluentValidationWikify.Integration
{
    public class WithApi
    {
        private static readonly Lazy<IClassDocumenter> ClassDocumenter = new Lazy<IClassDocumenter>(() =>
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            return container.Resolve<IClassDocumenter>();
        });

        public IClassDocumenter Init()
        {
            return ClassDocumenter.Value;
        }
    }
}
