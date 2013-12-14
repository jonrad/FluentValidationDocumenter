using FluentValidationWikify.Documenters;
using FluentValidationWikify.Models;
using Machine.Specifications;

namespace FluentValidationWikify.Specs
{
    public class SimpleSentenceDocumenterSpecs
    {
        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_no_details : with_documenter
        {
            Establish context = () =>
                rule = new Rule("Name");

            Because of = () =>
                result = Documenter.ToString(rule);

            It should_return_empty_string = () =>
                result.ShouldEqual(string.Empty);

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_required_token : with_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("required"));

            Because of = () =>
                result = Documenter.ToString(rule);

            It should_return_required = () =>
                result.ShouldEqual("Name is required");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_must_token : with_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"));

            Because of = () =>
                result = Documenter.ToString(rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_must_token_and_required_token : with_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("required"));

            Because of = () =>
                result = Documenter.ToString(rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome and is required");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_when_and_must_tokens : with_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("when", "NotEmpty"));

            Because of = () =>
                result = Documenter.ToString(rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome when not empty");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_two_when_and_must_tokens : with_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("when", "NotEmpty"), new Token("when", "NotFunny"));

            Because of = () =>
                result = Documenter.ToString(rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome when not empty and when not funny");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceDocumenter))]
        public class with_documenter
        {
            Establish context = () =>
                Documenter = new SimpleSentenceDocumenter();

            protected static SimpleSentenceDocumenter Documenter;
        }
    }
}