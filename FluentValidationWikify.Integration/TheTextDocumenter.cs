using System;
using System.Linq;
using NUnit.Framework;

namespace FluentValidationWikify.Integration
{
    [TestFixture]
    public class TheTextDocumenter : WithApi
    {
        [Test]
        public void ForSingleModelAndRule()
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

        [Test]
        public void ForMultipleRules()
        {
            const string Text = @"
                public class ModelValidator : AbstractValidator<Model>
                {
                    public ModelValidator()
                    {
                        RuleFor(m => m.A).NotNull();
                        RuleFor(m => m.B).NotNull();
                        RuleFor(m => m.C).NotNull();
                        RuleFor(m => m.D).NotNull();
                        RuleFor(m => m.E).NotNull();
                    }
                }";

            var documenter = InitDocumenter();

            var results = documenter.ToString(Text);

            Assert.That(
                results,
                Is.EqualTo(
                    "Rules for Model" + Environment.NewLine +
                    string.Join(
                        Environment.NewLine,
                        Enumerable.Range(0, 5).Select(i => string.Format("{0} is required", (char)('A' + i))))));
        }

        [Test]
        public void ForSinglePredicate()
        {
            const string Text = @"
                public class ModelValidator : AbstractValidator<Model>
                {
                    public ModelValidator()
                    {
                        RuleFor(m => m.Age).Must(x => x == 25);
                    }
                }";

            var documenter = InitDocumenter();

            var results = documenter.ToString(Text);

            Assert.That(
                results,
                Is.EqualTo(
                    "Rules for Model" + Environment.NewLine +
                    "Age must satisfy age == 25"));
        }
    }
}