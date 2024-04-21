using FluentValidation;
using UBB_Trips.ViewModels;

public class TripViewModelValidator : AbstractValidator<TripViewModel>
{
    public TripViewModelValidator()
    {
        RuleFor(x => x.TripSTART)
            .NotEmpty().WithMessage("Start Date is required")
            .Must(date => date > DateTime.Now).WithMessage("Start Date must be in the future");

        RuleFor(x => x.TripEND)
            .NotEmpty().WithMessage("End Date is required")
            .Must((model, endDate) => endDate > model.TripSTART).WithMessage("End Date must be after Start Date")
            .Must((model, endDate) => (endDate - model.TripSTART).TotalDays <= 365).WithMessage("Trip duration cannot exceed 1 year");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

        RuleFor(x => x.ImageURL)
            .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("Invalid Image URL format")
            .When(x => !string.IsNullOrEmpty(x.ImageURL));

        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid Trip Type");
        RuleFor(x => x.Food).IsInEnum().WithMessage("Invalid Food and Accommodation");
        RuleFor(x => x.Country).IsInEnum().WithMessage("Invalid Country");
    }
}
