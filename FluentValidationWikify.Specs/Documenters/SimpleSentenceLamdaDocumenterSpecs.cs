using FluentValidationWikify.Documenters;
using Machine.Specifications;

namespace FluentValidationWikify.Specs.Documenters
{
    public class SimpleSentenceLamdaDocumenterSpecs
    {
        [Subject(typeof(SimpleSentenceLamdaDocumenter))]
        public class when_documenting
        {
            Establish context = () =>
                documenter = new SimpleSentenceLamdaDocumenter();

            Because of = () =>
                result = documenter.Document("person", Tokens.AgeLessThan25);

            It should_be_friendly = () =>
                result.ShouldEqual("person.age<25");

            static SimpleSentenceLamdaDocumenter documenter;

            static string result;
        }
    }
}