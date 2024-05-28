using FluentValidation.TestHelper;
using UBB_Trips.ViewModels;
using UBB_Trips.Validators;
using Xunit;
using System;
using System.Diagnostics.Metrics;
using UBB_Trips.Models;

namespace UBB_Trips.Tests.ValidatorTests
{
    public class TripViewModelValidatorTests
    {
        private readonly TripViewModelValidator _validator;

        public TripViewModelValidatorTests()
        {
            _validator = new TripViewModelValidator();
        }

        [Fact]

        public void Should_Have_Error_When_TripSTART_Is_Empty()
        {
            var model = new TripViewModel { TripSTART = default };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TripSTART)
                .WithErrorMessage("Start Date is required");
        }

        [Fact]
        public void Should_Have_Error_When_TripSTART_Is_Not_In_Future()
        {
            var model = new TripViewModel { TripSTART = DateTime.Now.AddMinutes(-1) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TripSTART)
                .WithErrorMessage("Start Date must be in the future");
        }

        [Fact]
        public void Should_Have_Error_When_TripEND_Is_Empty()
        {
            var model = new TripViewModel { TripEND = default };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TripEND)
                .WithErrorMessage("End Date is required");
        }

        [Fact]
        public void Should_Have_Error_When_TripEND_Is_Before_TripSTART()
        {
            var model = new TripViewModel
            {
                TripSTART = DateTime.Now.AddDays(1),
                TripEND = DateTime.Now
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TripEND)
                .WithErrorMessage("End Date must be after Start Date");
        }

        [Fact]
        public void Should_Have_Error_When_Trip_Duration_Exceeds_One_Year()
        {
            var model = new TripViewModel
            {
                TripSTART = DateTime.Now.AddDays(1),
                TripEND = DateTime.Now.AddDays(367)
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TripEND)
                .WithErrorMessage("Trip duration cannot exceed 1 year");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var model = new TripViewModel { Title = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title)
                .WithErrorMessage("Title is required");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_100_Characters()
        {
            var model = new TripViewModel { Title = new string('a', 101) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Title)
                .WithErrorMessage("Title cannot exceed 100 characters");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Empty()
        {
            var model = new TripViewModel { Description = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description is required");
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_500_Characters()
        {
            var model = new TripViewModel { Description = new string('a', 501) };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Description)
                .WithErrorMessage("Description cannot exceed 500 characters");
        }

        [Fact]
        public void Should_Have_Error_When_ImageURL_Is_Invalid()
        {
            var model = new TripViewModel { ImageURL = "invalid_url" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.ImageURL)
                .WithErrorMessage("Invalid Image URL format");
        }

        [Fact]
        public void Should_Not_Have_Error_When_ImageURL_Is_Valid()
        {
            var model = new TripViewModel { ImageURL = "https://example.com/image.jpg" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.ImageURL);
        }


        [Fact]
        public void Should_Have_Error_When_Food_Is_Invalid()
        {
            var model = new TripViewModel { Food = (FoodAcoomodationModel)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Food)
                .WithErrorMessage("Invalid Food and Accommodation");
        }

        [Fact]
        public void Should_Have_Error_When_Country_Is_Invalid()
        {
            var model = new TripViewModel { Country = (CountryModel)999 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Country)
                .WithErrorMessage("Invalid Country");
        }
    }
}
