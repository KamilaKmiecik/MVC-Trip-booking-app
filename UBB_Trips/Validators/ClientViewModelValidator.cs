using FluentValidation;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Validators
{
    public class ClientViewModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientViewModelValidator()
        {
            RuleFor(client => client.FirstName)
                .NotEmpty().WithMessage("First name is required");

            RuleFor(client => client.LastName)
                .NotEmpty().WithMessage("Last name is required");

            RuleFor(client => client.Email)
                .EmailAddress().WithMessage("Invalid email address");
        }
    }
}
