using System.Collections.Generic;
using System.Linq;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class ClassDocumenter : IClassDocumenter
    {
        private readonly Visitor visitor;

        public ClassDocumenter(IRuleDocumenter ruleDocumenter)
        {
            visitor = new Visitor(ruleDocumenter);
        }

        public bool CanProcess(ClassDeclarationSyntax node)
        {
            return true;
        }

        public ClassRules Get(ClassDeclarationSyntax node)
        {
            var name = node
                .ChildNodes().OfType<BaseListSyntax>().First()
                .ChildNodes().OfType<GenericNameSyntax>().First()
                .ChildNodes().OfType<TypeArgumentListSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.ValueText;

            var rules = new ClassRules(name);

            rules.AddRange(visitor.Visit(node));

            return rules;
        }

        private class Visitor : SyntaxVisitor<IEnumerable<Rule>>
        {
            private readonly IRuleDocumenter ruleDocumenter;

            public Visitor(IRuleDocumenter ruleDocumenter)
            {
                this.ruleDocumenter = ruleDocumenter;
            }

            public override IEnumerable<Rule> VisitClassDeclaration(ClassDeclarationSyntax node)
            {
                return RecursivelyVisit(node);
            }

            public override IEnumerable<Rule> VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
            {
                return RecursivelyVisit(node);
            }

            public override IEnumerable<Rule> VisitBlock(BlockSyntax node)
            {
                return ruleDocumenter.Get(node);
            }

            private IEnumerable<Rule> RecursivelyVisit(SyntaxNode node)
            {
                return node.ChildNodes().Select(Visit).Where(c => c != null).SelectMany(c => c);
            }
        }
    }
}