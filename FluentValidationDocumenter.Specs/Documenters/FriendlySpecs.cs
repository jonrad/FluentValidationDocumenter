﻿using FluentValidationDocumenter.Documenters;
using Machine.Specifications;

namespace FluentValidationDocumenter.Specs.Documenters
{
    public class FriendlySpecs
    {
        [Subject(typeof(Friendly))]
        class when_parsing_simple : with_friendly
        {
            It should_return_friendly = () =>
                Friendly.Get("Age").ShouldEqual("age");
        }

        [Subject(typeof(Friendly))]
        class when_parsing_camel_case : with_friendly
        {
            It should_return_friendly = () =>
                Friendly.Get("dateOfBirth").ShouldEqual("date of birth");
        }

        [Subject(typeof(Friendly))]
        class when_parsing_pascal_case : with_friendly
        {
            It should_return_friendly = () =>
                Friendly.Get("DateOfBirth").ShouldEqual("date of birth");
        }

        [Subject(typeof(Friendly))]
        class when_parsing_underscores_case : with_friendly
        {
            It should_return_friendly = () =>
                Friendly.Get("date_of_birth").ShouldEqual("date of birth");
        }

        class with_friendly
        {
            Establish context = () =>
                Friendly = new Friendly();

            protected static Friendly Friendly;
        }
    }
}