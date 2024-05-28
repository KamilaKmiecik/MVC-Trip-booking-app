using FluentValidation.TestHelper;
using UBB_Trips.ViewModels;
using UBB_Trips.Validators;
using Xunit;

namespace UBB_Trips.Tests.ViewModelValidatorTests
{
    public class ClientViewModelValidatorTests
    {
        private readonly ClientViewModelValidator _validator;

        public ClientViewModelValidatorTests()
        {
            _validator = new ClientViewModelValidator();
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            var model = new ClientViewModel { FirstName = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(client => client.FirstName)
                .WithErrorMessage("First name is required");
        }

        [Fact]
        public void Should_Not_Have_Error_When_FirstName_Is_Not_Empty()
        {
            var model = new ClientViewModel { FirstName = "Kamila" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(client => client.FirstName);
        }

        [Fact]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            var model = new ClientViewModel { LastName = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(client => client.LastName)
                .WithErrorMessage("Last name is required");
        }

        [Fact]
        public void Should_Not_Have_Error_When_LastName_Is_Not_Empty()
        {
            var model = new ClientViewModel { LastName = "Kmiecik" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(client => client.LastName);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var model = new ClientViewModel { Email = "invalid-email :)" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(client => client.Email)
                .WithErrorMessage("Invalid email address");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Email_Is_Valid()
        {
            var model = new ClientViewModel { Email = "kamila.k@example.com" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(client => client.Email);
        }
    }
}
