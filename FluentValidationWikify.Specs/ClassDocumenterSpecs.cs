using System.Linq;
using Machine.Specifications;
using Roslyn.Compilers.CSharp;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(ClassDocumenter))]
    public class ClassDocumenterSpecs
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

                documenter = new ClassDocumenter();
            };

            Because of = () =>
                classRules = documenter.Get(node);

            It should_be_able_to_process = () =>
                documenter.CanProcess(node).ShouldBeTrue();

            It should_return_required = () =>
                classRules.Name.ShouldEqual("Model");

            It should_have_no_rules = () =>
                classRules.Count.ShouldEqual(0);

            static ClassRules classRules;

            static ClassDocumenter documenter;

            static ClassDeclarationSyntax node;
        }
    }
}