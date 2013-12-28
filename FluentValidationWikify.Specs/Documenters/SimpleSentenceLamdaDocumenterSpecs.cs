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
                documenter = new SimpleSentenceLamdaDocumenter("Person");

            Because of = () =>
                result = documenter.Document(Tokens.AgeLessThan25);

            It should_be_friendly = () =>
                result.ShouldEqual("Person's age < 25");

            static SimpleSentenceLamdaDocumenter documenter;

            static string result;
        }
    }
}