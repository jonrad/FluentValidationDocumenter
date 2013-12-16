using System;
using System.Linq;
using NUnit.Framework;

namespace FluentValidationWikify.Integration
{
    [TestFixture]
    public class TheClassDocumenter : WithApi
    {
        [Test]
        public void OutputsExpectedValues()
        {
            const string Text = @"
                public class ModelValidator : AbstractValidator<Model>
                {
                    public ModelValidator()
                    {
                        RuleFor(m => m.Name).NotNull();
                    }
                }";

            var documenter = InitDocumenter();

            var results = documenter.ToString(Text);

            Assert.That(results, Is.EqualTo(@"Rules for Model" + Environment.NewLine + "Name is required"));
        }
    }
}