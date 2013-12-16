using System;
using System.Linq;

namespace FluentValidationWikify.Documenters
{
    public class SimpleSentenceTextDocumenter : ITextDocumenter
    {
        private readonly ITextTokenizer textTokenizer;

        private readonly IClassDocumenter classDocumenter;

        public SimpleSentenceTextDocumenter(
            ITextTokenizer textTokenizer,
            IClassDocumenter classDocumenter)
        {
            this.textTokenizer = textTokenizer;
            this.classDocumenter = classDocumenter;
        }

        public string ToString(string text)
        {
            var classes = textTokenizer.Get(text);

            return string.Join(
                Environment.NewLine + Environment.NewLine,
                classes.Select(c => classDocumenter.ToString(c)));
        }
    }
}