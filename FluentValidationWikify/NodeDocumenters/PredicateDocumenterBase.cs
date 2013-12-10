﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.NodeDocumenters
{
    public abstract class PredicateDocumenterBase : MemberAccessExpressionDocumenter
    {
        public override bool IsNewRule
        {
            get { return false; }
        }

        public override string Get(SyntaxNode node)
        {
            var valueText = node
                .ChildNodes().OfType<ArgumentListSyntax>().First()
                .ChildNodes().OfType<ArgumentSyntax>().First()
                .ChildNodes().OfType<IdentifierNameSyntax>().First()
                .Identifier.ValueText;

            if (valueText == null)
            {
                throw new NotImplementedException();
            }

            valueText = Regex.Replace(valueText, "([a-z])([A-Z])", "$1 $2");
            return MethodName + " " + valueText;
        }
    }
}