using System;
using System.Linq;
using NUnit.Framework;

namespace FluentValidationDocumenter.Integration
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

        [Test]
        public void ForPredicateAndWhere()
        {
            const string Text = @"
                public class PersonValidator : AbstractValidator<Person>
                {
                    public PersonValidator()
                    {
                        RuleFor(m => m.Age).Must(x => x == 25).When(m => m.Name == ""Jon"");
                    }
                }";

            var documenter = InitDocumenter();

            var results = documenter.ToString(Text);

            Assert.That(
                results,
                Is.EqualTo(
                    "Rules for Person" + Environment.NewLine +
                    @"Age must satisfy age == 25 when person.name == ""Jon"""));
        }

        [Test]
        public void ForMultiplePredicateAndWhere()
        {
            const string Text = @"
                public class PersonValidator : AbstractValidator<Person>
                {
                    public PersonValidator()
                    {
                        RuleFor(m => m.Age).Must(x => x == 25).When(m => m.Name == ""Jon"");
                        RuleFor(m => m.Name).Must(x => x == ""Jon"").When(m => m.Age == 25);
                        RuleFor(m => m.City).Must(x => x == ""New York"").When(m => m.State == ""NY"");
                    }
                }";

            var documenter = InitDocumenter();

            var results = documenter.ToString(Text);

            Assert.That(
                results,
                Is.EqualTo(
                    "Rules for Person"
                    + Environment.NewLine + @"Age must satisfy age == 25 when person.name == ""Jon""" 
                    + Environment.NewLine + @"Name must satisfy name == ""Jon"" when person.age == 25"
                    + Environment.NewLine + @"City must satisfy city == ""New York"" when person.state == ""NY"""));
        }

        [Test]
        public void ForNestedWhen()
        {
            const string Text = @"
                public class PersonValidator : AbstractValidator<Person>
                {
                    public PersonValidator()
                    {
                        When(x => x.Id > 0, () => {
                           RuleFor(x => x.Surname).NotNull();
                           RuleFor(x => x.Forename).NotNull();
                        });
                    }
                }";

            var documenter = InitDocumenter();
            var results = documenter.ToString(Text);

            const string ExpectedResults = 
@"Rules for Person
Surname is required when person.id > 0
Forename is required when person.id > 0";

            Assert.That(results, Is.EqualTo(ExpectedResults));
        }
    }
}