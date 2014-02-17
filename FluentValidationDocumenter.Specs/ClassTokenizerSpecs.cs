using System.Linq;
using FluentValidationWikify.Models;
using FluentValidationWikify.Tokenizers;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    public class ClassTokenizerSpecs : WithFakes
    {
        [Subject(typeof(ClassTokenizer))]
        class with_empty_class
        {
            Establish context = () =>
            {
                node =
                    SyntaxTree.ParseText("class ModelValidator:AbstractValidator<Model>{}")
                        .GetRoot()
                        .DescendantNodes()
                        .OfType<ClassDeclarationSyntax>()
                        .First();

                documenter = new ClassTokenizer(The<IRuleTokenizer>());
            };

            Because of = () =>
                classRules = documenter.Get(node);

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_required = () =>
                classRules.Name.ShouldEqual("Model");

            It should_have_no_rules = () =>
                classRules.Count.ShouldEqual(0);

            It should_have_attempted_to_get_rules = () =>
                The<IRuleTokenizer>().WasNotToldTo(r => r.Get(Param.IsAny<SyntaxNode>()));

            static ClassRules classRules;

            static ClassTokenizer documenter;

            static ClassDeclarationSyntax node;
        }

        [Subject(typeof(ClassTokenizer))]
        class with_class_containing_ctor_rules
        {
            Establish context = () =>
            {
                const string text = @"
                    public class ModelValidator : AbstractValidator<Model>
                    {
                        public ModelValidator()
                        {
                            RuleFor(m => m.Name).NotEmpty();
                        }
                    }";

                node =
                    SyntaxTree.ParseText(text)
                        .GetRoot()
                        .DescendantNodes()
                        .OfType<ClassDeclarationSyntax>()
                        .First();

                documenter = new ClassTokenizer(The<IRuleTokenizer>());
            };

            Because of = () =>
                classRules = documenter.Get(node);

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_required = () =>
                classRules.Name.ShouldEqual("Model");

            It should_have_attempted_to_get_rules = () =>
                The<IRuleTokenizer>().WasToldTo(r => r.Get(Param.IsAny<SyntaxNode>())).OnlyOnce();

            static ClassRules classRules;

            static ClassTokenizer documenter;

            static ClassDeclarationSyntax node;
        }
    }
}