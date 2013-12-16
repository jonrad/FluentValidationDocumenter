using System;
using FluentValidationWikify.Documenters;
using FluentValidationWikify.Models;
using Machine.Fakes;
using Machine.Specifications;

namespace FluentValidationWikify.Specs
{
    public class SimpleSentenceTextDocumenterSpecs
    {
        [Subject(typeof (SimpleSentenceTextDocumenter))]
        public class with_no_details : WithSubject<SimpleSentenceTextDocumenter>
        {
            Establish context = () =>
            {
                The<IClassDocumenter>().WhenToldTo(c => c.ToString(Param.IsAny<ClassRules>()))
                    .Return<ClassRules>(c => c.Name);

                The<ITextTokenizer>().WhenToldTo(t => t.Get(Param.IsAny<string>()))
                    .Return(new[] { new ClassRules("Model"), new ClassRules("Person") });

                text = @"
                    public class ModelValidator : AbstractValidator<Model>
                    {
                        public ModelValidator()
                        {
                            RuleFor(m => m.Name).NotEmpty();
                        }
                    }
                    public class PersonValidator : AbstractValidator<Person>
                    {
                        public PersonValidator()
                        {
                            RuleFor(m => m.Name).NotEmpty();
                        }
                    }
";
            };

            Because of = () =>
                results = Subject.ToString(text);

            It returned_expected_results = () =>
                results.ShouldEqual("Model" + Environment.NewLine + Environment.NewLine + "Person");

            static string text;

            static string results;
        }
    }

    public class SimpleSentenceClassDocumenterSpecs
    {
        [Subject(typeof(SimpleSentenceClassDocumenter))]
        public class with_no_details : WithSubject<SimpleSentenceClassDocumenter>
        {
            private Establish context = () =>
                classRules = new ClassRules("Person");

            Because of = () =>
                result = Subject.ToString(classRules);

            It should_return_empty_string = () =>
                result.ShouldEqual(string.Empty);

            static string result;

            static ClassRules classRules;
        }

        [Subject(typeof(SimpleSentenceClassDocumenter))]
        public class with_details : WithSubject<SimpleSentenceClassDocumenter>
        {
            Establish context = () =>
            {
                The<IRuleDocumenter>().WhenToldTo(d => d.ToString(Param.IsAny<Rule>()))
                    .Return<Rule>(r => r.Name);

                classRules = new ClassRules("Person")
                {
                    new Rule("A"),
                    new Rule("B")
                };
            };

            Because of = () =>
                result = Subject.ToString(classRules);

            It should_return_empty_string = () =>
                result.ShouldEqual("Rules for Person" + Environment.NewLine + "A" + Environment.NewLine + "B");

            static string result;

            static ClassRules classRules;
        }
    }
}