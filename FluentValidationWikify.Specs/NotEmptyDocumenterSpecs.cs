﻿using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Machine.Specifications;

namespace FluentValidationWikify.Specs
{
    [Subject(typeof(NotEmptyDocumenter))]
    public class NotEmptyDocumenterSpecs
    {
        Establish context = () =>
        {
            var validator = new InlineValidator<Model>();
            validator.RuleFor(v => v.Name).NotEmpty();

            propertyValidator = validator.CreateDescriptor().GetValidatorsForMember("Name").First();

            documenter = new NotEmptyDocumenter();
        };

        It should_be_able_to_process =
            () => documenter.CanProcess(propertyValidator).ShouldBeTrue();

        It should_return_required =
            () => documenter.Get(propertyValidator).ShouldEqual("Required");

        static IPropertyValidator propertyValidator;

        static NotEmptyDocumenter documenter;
    }
}
