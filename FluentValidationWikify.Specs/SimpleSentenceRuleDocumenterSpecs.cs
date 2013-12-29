using FluentValidationWikify.Documenters;
using FluentValidationWikify.Models;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class SimpleSentenceRuleDocumenterSpecs
    {
        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_no_details : the_documenter
        {
            Establish context = () =>
                rule = new Rule("Name");

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_empty_string = () =>
                result.ShouldEqual(string.Empty);

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_required_token : the_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("required"));

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_required = () =>
                result.ShouldEqual("Name is required");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_must_token : the_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"));

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_equal_token : the_documenter
        {
            Establish context = () =>
                rule = new Rule("Age", new Token("equal", 100));

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_required = () =>
                result.ShouldEqual("Age must equal 100");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_must_token_and_required_token : the_documenter
        {
            Establish context = () =>
            {
                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("required"));
            };

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome and is required");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_when_and_must_tokens : the_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("when", "NotEmpty"));

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome when not empty");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_must_token_with_lamda : the_documenter
        {
            Establish context = () =>
            {
                The<ILamdaDocumenter>()
                    .WhenToldTo(l => l.Document(Param.IsAny<string>(), Param.IsAny<SimpleLambdaExpressionSyntax>()))
                    .Return("age < 25");

                rule = new Rule("Name", new Token("must", Tokens.AgeLessThan25));
            };

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_non_null = () =>
                result.ShouldNotBeNull();

            It should_return_semi_friendly_name = () =>
                result.ShouldEqual("Name must satisfy age < 25");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_when_token_with_lamda : the_documenter
        {
            Establish context = () =>
            {
                The<ILamdaDocumenter>()
                    .WhenToldTo(l => l.Document(Param.IsAny<string>(), Param.IsAny<SimpleLambdaExpressionSyntax>()))
                    .Return("lamda_parsed");

                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("when", Tokens.AgeLessThan25));
            };

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_non_null = () =>
                result.ShouldNotBeNull();

            It should_return_semi_friendly_name = () =>
                result.ShouldEqual("Name must be awesome when lamda_parsed");

            static string result;

            static Rule rule;
        }

        [Subject(typeof(SimpleSentenceRuleDocumenter))]
        public class with_two_when_and_must_tokens : the_documenter
        {
            Establish context = () =>
                rule = new Rule("Name", new Token("must", "BeAwesome"), new Token("when", "NotEmpty"), new Token("when", "NotFunny"));

            Because of = () =>
                result = Subject.Document("X", rule);

            It should_return_required = () =>
                result.ShouldEqual("Name must be awesome when not empty and when not funny");

            static string result;

            static Rule rule;
        }

        public class the_documenter : WithSubject<SimpleSentenceRuleDocumenter>
        {
            Establish context = () =>
            {
                The<IFriendly>().WhenToldTo(f => f.Get("BeAwesome")).Return("be awesome");
                The<IFriendly>().WhenToldTo(f => f.Get("NotEmpty")).Return("not empty");
                The<IFriendly>().WhenToldTo(f => f.Get("NotFunny")).Return("not funny");
            };
        }
    }
}