using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FluentValidationWikify.Console.Shells;
using FluentValidationWikify.Documenters;
using FluentValidationWikify.NodeTokenizers;
using FluentValidationWikify.Tokenizers;

namespace FluentValidationWikify.Console.Installers
{
    public class AppInstaller : IWindsorInstaller
    {
        private readonly bool force;

        public AppInstaller(bool force)
        {
            this.force = force;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(
                Component.For<IFriendly>().ImplementedBy<Friendly>(),
                Component.For<ILamdaDocumenter>().ImplementedBy<SimpleSentenceLamdaDocumenter>(),
                Component.For<IShell>().ImplementedBy<MainShell>(),
                Component.For<ITextTokenizer>().ImplementedBy<TextTokenizer>(),
                Component.For<IClassTokenizer>().ImplementedBy<ClassTokenizer>(),
                Component.For<IRuleTokenizer>().ImplementedBy<RuleTokenizer>().DependsOn(Dependency.OnValue("force", force)),
                Component.For<IRuleDocumenter>().ImplementedBy<SimpleSentenceRuleDocumenter>(),
                Component.For<IClassDocumenter>().ImplementedBy<SimpleSentenceClassDocumenter>(),
                Component.For<ITextDocumenter>().ImplementedBy<SimpleSentenceTextDocumenter>(),
                Classes.FromAssembly(typeof(INodeTokenizer).Assembly)
                    .BasedOn<INodeTokenizer>()
                    .WithService.FromInterface(typeof(INodeTokenizer)));
        }
    }
}