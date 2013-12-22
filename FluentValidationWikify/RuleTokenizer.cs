﻿using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using FluentValidationWikify.Models;
using FluentValidationWikify.NodeTokenizers;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify
{
    public class RuleTokenizer : IRuleTokenizer
    {
        private readonly Visitor visitor;

        public RuleTokenizer(IEnumerable<INodeTokenizer> documenters)
        {
            visitor = new Visitor(documenters);

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
                Logger.DebugFormat("Using {0}", handler.Tokenizer.GetType());

                if (handler.Tokenizer.IsNewRule)
                {
                    if (rule != null)
                    {
                        yield return rule;
                    }

                    details = new List<Token>();
                    rule = new Rule
                    {
                        Name = handler.Tokenizer.Get(handler.Node).Info.ToString(), // FIX
                        Details = details
                    };
                }
                else if (rule != null)
                {
                    details.Add(handler.Tokenizer.Get(handler.Node));
                }
            }

            if (rule != null)
            {
                yield return rule;
            }
        }

        private class Visitor : SyntaxVisitor<IEnumerable<Handler>>
        {
            private readonly IEnumerable<INodeTokenizer> documenters;

            public Visitor(IEnumerable<INodeTokenizer> documenters)
            {
                this.documenters = documenters;
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

                var documenter = documenters.FirstOrDefault(d => d.CanProcess(node));

                if (documenter != null)
                {
                    yield return new Handler
                    {
                        Node = node,
                        Tokenizer = documenter
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