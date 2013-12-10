using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidationWikify.NodeDocumenters;

namespace FluentValidationWikify.Integration
{
    public class AppInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(
                Component.For<ITextDocumenter>().ImplementedBy<TextDocumenter>(),
                Component.For<IClassDocumenter>().ImplementedBy<ClassDocumenter>(),
                Component.For<IRuleDocumenter>().ImplementedBy<RuleDocumenter>(),
                Classes.FromAssembly(typeof(INodeDocumenter).Assembly).BasedOn<INodeDocumenter>().WithService.FromInterface(typeof(INodeDocumenter)));
        }
    }
}