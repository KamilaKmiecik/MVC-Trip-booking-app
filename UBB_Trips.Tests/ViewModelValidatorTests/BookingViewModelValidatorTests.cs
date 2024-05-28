using FluentValidation.TestHelper;
using UBB_Trips.ViewModels;
using UBB_Trips.Validators;
using Xunit;

namespace UBB_Trips.Tests.ValidatorTests
{
    public class BookingViewModelValidatorTests
    {
        private readonly BookingViewModelValidator _validator;

        public BookingViewModelValidatorTests()
        {
            _validator = new BookingViewModelValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var model = new BookingViewModel { Name = string.Empty };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Name is required!");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Not_Empty()
        {
            var model = new BookingViewModel { Name = "Kamila Kmiecik" };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }


        [Fact]
        public void Should_Not_Have_Error_When_TripID_Is_Not_Empty()
        {
            var model = new BookingViewModel { TripID = 1 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.TripID);
        }

        [Fact]
        public void Should_Have_Error_When_NumberOfBookings_Is_Zero()
        {
            var model = new BookingViewModel { NumberOfBookings = 0 };

            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.NumberOfBookings)
                .WithErrorMessage("Number of people must be greater than 0!");
        }

        [Fact]
        public void Should_Not_Have_Error_When_NumberOfBookings_Is_Greater_Than_Zero()
        {
            var model = new BookingViewModel { NumberOfBookings = 1 };

            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(x => x.NumberOfBookings);
        }
    }
}
