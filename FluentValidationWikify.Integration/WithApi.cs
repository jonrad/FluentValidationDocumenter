using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace FluentValidationWikify.Integration
{
    public class WithApi
    {
        private static readonly Lazy<ITextDocumenter> TextDocumenter = new Lazy<ITextDocumenter>(() =>
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            return container.Resolve<ITextDocumenter>();
        });

        public ITextDocumenter Init()
        {
            return TextDocumenter.Value;
        }
    }
}
