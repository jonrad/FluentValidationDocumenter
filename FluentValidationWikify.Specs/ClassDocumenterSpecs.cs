using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(ClassDocumenter))]
    public class ClassDocumenterSpecs : WithFakes
    {
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

                documenter = new ClassDocumenter(The<IRuleDocumenter>());
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
                The<IRuleDocumenter>().WasNotToldTo(r => r.Get(Param.IsAny<SyntaxNode>()));

            static ClassRules classRules;

            static ClassDocumenter documenter;

            static ClassDeclarationSyntax node;
        }

        class with_class_containing_ctor_rules
        {
            Establish context = () =>
            {
                var text = @"
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

                documenter = new ClassDocumenter(The<IRuleDocumenter>());
            };

            Because of = () =>
                classRules = documenter.Get(node);

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_required = () =>
                classRules.Name.ShouldEqual("Model");

            It should_have_no_rules = () =>
                classRules.Count.ShouldEqual(1);

            It should_have_attempted_to_get_rules = () =>
                The<IRuleDocumenter>().WasToldTo(r => r.Get(Param.IsAny<SyntaxNode>())).OnlyOnce();

            static ClassRules classRules;

            static ClassDocumenter documenter;

            static ClassDeclarationSyntax node;
        }
    }
}