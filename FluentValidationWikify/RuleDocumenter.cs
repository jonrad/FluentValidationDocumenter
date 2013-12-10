using System.Collections.Generic;
using System.Linq;
using FluentValidationWikify.Models;
using FluentValidationWikify.NodeDocumenters;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class RuleDocumenter : IRuleDocumenter
    {
        private readonly Visitor visitor;

        public RuleDocumenter(IEnumerable<INodeDocumenter> documenters)
        {
            visitor = new Visitor(documenters);
        }

        public IEnumerable<Rule> Get(SyntaxNode tree)
        {
            Rule rule = null;
            List<string> details = null;

            foreach (var handler in visitor.Visit(tree))
            {
                if (handler.Documenter.IsNewRule)
                {
                    if (rule != null)
                    {
                        yield return rule;
                    }

                    details = new List<string>();
                    rule = new Rule
                    {
                        Name = handler.Documenter.Get(handler.Node),
                        Details = details
                    };
                }
                else if (rule != null)
                {
                    details.Add(handler.Documenter.Get(handler.Node));
                }
            }

            if (rule != null)
            {
                yield return rule;
            }
        }

        private class Visitor : SyntaxVisitor<IEnumerable<Handler>>
        {
            private readonly IEnumerable<INodeDocumenter> documenters;

            public Visitor(IEnumerable<INodeDocumenter> documenters)
            {
                this.documenters = documenters;
            }

            public override IEnumerable<Handler> VisitBlock(BlockSyntax node)
            {
                return RecursivelyVisit(node);
            }

            public override IEnumerable<Handler> VisitExpressionStatement(ExpressionStatementSyntax node)
            {
                return RecursivelyVisit(node);
            }

            public override IEnumerable<Handler> VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
            {
                return RecursivelyVisit(node);
            }

            public override IEnumerable<Handler> VisitInvocationExpression(InvocationExpressionSyntax node)
            {
                return RecursivelyVisit(node);
            }

            private IEnumerable<Handler> RecursivelyVisit(SyntaxNode node)
            {
                foreach (var result in node.ChildNodes().Select(Visit).Where(v => v != null).SelectMany(v => v))
                {
                    yield return result;
                }

                foreach (var documenter in documenters)
                {
                    if (documenter.CanProcess(node))
                    {
                        yield return new Handler
                        {
                            Node = node,
                            Documenter = documenter
                        };

                        break;
                    }
                }
            }
        }

        private class Handler
        {
            public SyntaxNode Node { get; set; }

            public INodeDocumenter Documenter { get; set; }
        }
    }
}