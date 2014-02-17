using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using FluentValidationDocumenter.Documenters;
using FluentValidationDocumenter.Tokenizers;

namespace FluentValidationDocumenter.Integration
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

        private static readonly Lazy<ITextDocumenter> TextDocumenter = new Lazy<ITextDocumenter>(() =>
        {
            var container = Container.Value;
            return container.Resolve<ITextDocumenter>();
        });

        public ITextTokenizer InitTokenizer()
        {
            return TextTokenizer.Value;
        }

        public ITextDocumenter InitDocumenter()
        {
            return TextDocumenter.Value;
        }
    }
}
