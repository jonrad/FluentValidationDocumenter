using System.Linq;
using NUnit.Framework;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Integration
{
    [TestFixture]
    public class TheClassDocumenter : WithApi
    {
        [Test]
        public void ShouldWork()
        {
            const string text = @"
                public class ModelValidator : AbstractValidator<Model>
                {
                    public ModelValidator()
                    {
                        RuleFor(m => m.Name).NotNull();
                    }
                }";
            var documenter = Init();

            var result =
                documenter.Get(
                    SyntaxTree.ParseText(text).GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First());

            Assert.That(result.Name, Is.EqualTo("Model"));

            Assert.That(result.Count, Is.EqualTo(1));

            Assert.That(result[0].Name, Is.EqualTo("Name"));

            Assert.That(result[0].Details.Count(), Is.EqualTo(1));

            Assert.That(result[0].Details.First(), Is.EqualTo("Required"));
        }
    }
}