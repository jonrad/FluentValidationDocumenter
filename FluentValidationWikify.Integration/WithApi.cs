using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidationWikify.Documenters;

namespace FluentValidationWikify.Integration
{
    public class WithApi
    {
        private static readonly Lazy<IWindsorContainer> Container = new Lazy<IWindsorContainer>(() =>
            new WindsorContainer().Install(FromAssembly.This()));

        private static readonly Lazy<ITextTokenizer> TextTokenizer = new Lazy<ITextTokenizer>(() =>
        {
            var container = Container.Value;
            return container.Resolve<ITextTokenizer>();
        });

        private static readonly Lazy<IClassDocumenter> TextDocumenter = new Lazy<IClassDocumenter>(() =>
        {
            var container = Container.Value;
            return container.Resolve<IClassDocumenter>();
        });

        public ITextTokenizer InitTokenizer()
        {
            return TextTokenizer.Value;
        }

        public IClassDocumenter InitDocumenter()
        {
            return TextDocumenter.Value;
        }
    }
}
