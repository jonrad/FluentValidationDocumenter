using System;
using FluentValidationDocumenter.Documenters;
using FluentValidationDocumenter.Models;
using FluentValidationDocumenter.Tokenizers;
using Machine.Fakes;
using Machine.Specifications;

namespace FluentValidationDocumenter.Specs
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
}