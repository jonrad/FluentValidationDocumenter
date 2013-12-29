using System.Collections.Generic;
using System.Linq;
using FluentValidationWikify.Models;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Tokenizers
{
    public class TextTokenizer : ITextTokenizer
    {
        private readonly IClassTokenizer classDocumenter;

        public TextTokenizer(IClassTokenizer classDocumenter)
        {
            this.classDocumenter = classDocumenter;
        }

        public IEnumerable<ClassRules> Get(string text)
        {
            var nodes = SyntaxTree.ParseText(text)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>();

            return nodes.Select(node => classDocumenter.Get(node));
        }
    }
}