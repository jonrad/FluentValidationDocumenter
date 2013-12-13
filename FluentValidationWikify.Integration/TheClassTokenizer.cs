using System.Linq;
using NUnit.Framework;

namespace FluentValidationWikify.Integration
{
    [TestFixture]
    public class TheTextDocumenter : WithApi
    {
        [Test]
        public void ShouldWork()
        {
            const string Text = @"
                public class ModelValidator : AbstractValidator<Model>
                {
                    public ModelValidator()
                    {
                        RuleFor(m => m.Name).NotNull();
                    }
                }";
            var documenter = Init();

            var results =
                documenter.Get(Text).ToArray();

            Assert.That(results.Length, Is.EqualTo(1));

            var result = results[0];

            Assert.That(result.Name, Is.EqualTo("Model"));

            Assert.That(result.Count, Is.EqualTo(1));

            Assert.That(result[0].Name, Is.EqualTo("Name"));

            Assert.That(result[0].Details.Count(), Is.EqualTo(1));

            Assert.That(result[0].Details.First().Id, Is.EqualTo("required"));
        }
    }
}