using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace FluentValidationWikify.Integration
{
    public class WithApi
    {
        private static readonly Lazy<ITextTokenizer> TextDocumenter = new Lazy<ITextTokenizer>(() =>
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            return container.Resolve<ITextTokenizer>();
        });

        public ITextTokenizer Init()
        {
            return TextDocumenter.Value;
        }
    }
}
