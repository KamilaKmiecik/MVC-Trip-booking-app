using FluentValidation;
using UBB_Trips.ViewModels;

namespace UBB_Trips.Validators;

public class BookingViewModelValidator : AbstractValidator<BookingViewModel>
{
    public BookingViewModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required!");
        RuleFor(x => x.TripID).NotEmpty().WithMessage("Trip is required!");
        RuleFor(x => x.NumberOfBookings).GreaterThan(0).WithMessage("Number of people must be greater than 0!");
    }
}
