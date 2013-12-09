﻿using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(NotEmptyDocumenter))]
    public class NotEmptyDocumenterSpecs
    {
        Establish context = () =>
        {
            node = 
                Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.IdentifierName("x"),
                        Syntax.IdentifierName("NotNull")));

            documenter = new NotEmptyDocumenter();
        };

        It should_be_able_to_process = () =>
            documenter.CanProcess(node).ShouldBeTrue();

        It should_return_required = () =>
            documenter.Get(node).ShouldEqual("Required");

        static INodeDocumenter documenter;

        static SyntaxNode node;
    }
}
