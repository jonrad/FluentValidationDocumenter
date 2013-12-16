using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidationWikify.Documenters;
using FluentValidationWikify.NodeTokenizers;

namespace FluentValidationWikify.Integration
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(
                Component.For<ITextTokenizer>().ImplementedBy<TextTokenizer>(),
                Component.For<IClassTokenizer>().ImplementedBy<ClassTokenizer>(),
                Component.For<IRuleTokenizer>().ImplementedBy<RuleTokenizer>(),
                Component.For<IRuleDocumenter>().ImplementedBy<SimpleSentenceRuleDocumenter>(),
                Component.For<IClassDocumenter>().ImplementedBy<SimpleSentenceClassDocumenter>(),
                Component.For<ITextDocumenter>().ImplementedBy<SimpleSentenceTextDocumenter>(),
                Classes.FromAssembly(typeof(INodeTokenizer).Assembly).BasedOn<INodeTokenizer>().WithService.FromInterface(typeof(INodeTokenizer)));
        }
    }
}