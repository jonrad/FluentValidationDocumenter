using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace FluentValidationWikify.Console.Installers
{
    public class DebugInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(
                Component.For<IShell>().ImplementedBy<DebugShell>());
        }
    }
}