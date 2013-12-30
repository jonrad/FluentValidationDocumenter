using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using FluentValidationWikify.Models;
using FluentValidationWikify.NodeTokenizers;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Tokenizers
{
    public class RuleTokenizer : IRuleTokenizer
    {
        private readonly bool force;

        private readonly Visitor visitor;

        public RuleTokenizer(IEnumerable<INodeTokenizer> nodeTokenizers, bool force = false)
        {
            this.force = force;
            visitor = new Visitor(nodeTokenizers);

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public IEnumerable<Rule> Get(SyntaxNode tree)
        {
            Rule rule = null;
            List<Token> details = null;

            visitor.Logger = Logger;

            foreach (var handler in visitor.Visit(tree))
            {
                Token token;

                Logger.DebugFormat("Using {0}", handler.Tokenizer.GetType());

                if (rule == null && !handler.Tokenizer.IsNewRule)
                {
                    Logger.WarnFormat("Unexpected node {0}", handler.Node);
                    continue;
                }

                try
                {
                    token = handler.Tokenizer.Get(handler.Node);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat(ex, "Could not parse node {0}", handler.Node);
                    if (force)
                    {
                        continue;
                    }

                    throw;
                }

                if (handler.Tokenizer.IsNewRule)
                {
                    if (rule != null)
                    {
                        yield return rule;
                    }

                    details = new List<Token>();
                    rule = new Rule
                    {
                        Name = token.Info.ToString(), // FIX
                        Details = details
                    };
                }
                else if (rule != null)
                {
                    details.Add(token);
                }
            }

            if (rule != null)
            {
                yield return rule;
            }
        }

        private class Visitor : SyntaxVisitor<IEnumerable<Handler>>
        {
            private readonly IEnumerable<INodeTokenizer> nodeTokenizers;

            public Visitor(IEnumerable<INodeTokenizer> nodeTokenizers)
            {
                this.nodeTokenizers = nodeTokenizers;
                Logger = NullLogger.Instance;
            }

            public ILogger Logger { private get; set; }

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

                var tokenizer = nodeTokenizers.FirstOrDefault(d => d.CanProcess(node));

                if (tokenizer != null)
                {
                    yield return new Handler
                    {
                        Node = node,
                        Tokenizer = tokenizer
                    };
                }
                else if (node is InvocationExpressionSyntax)
                {
                    var identifier = node
                        .ChildNodes()
                        .SelectMany(c => c.ChildNodes().OfType<IdentifierNameSyntax>())
                        .FirstOrDefault();

                    Logger.WarnFormat(
                        "Could not parse member access {0}", 
                        identifier == null ? node.GetText().ToString() : identifier.Identifier.Value);
                }
            }
        }

        private class Handler
        {
            public SyntaxNode Node { get; set; }

            public INodeTokenizer Tokenizer { get; set; }
        }
    }
}