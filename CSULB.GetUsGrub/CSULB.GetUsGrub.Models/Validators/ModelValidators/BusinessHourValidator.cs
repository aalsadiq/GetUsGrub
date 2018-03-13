using FluentValidation;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHourValidator</c> class.
    /// Defines rules to validate a BusinessHour.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class BusinessHourValidator : AbstractValidator<BusinessHour>
    {
        public BusinessHourValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.Day)
                    .NotEmpty().WithMessage("Business day is required.")
                    .NotNull().WithMessage("Business day is required.");

                RuleFor(x => x.OpenTime)
                    .NotEmpty().WithMessage("Open time is required.")
                    .NotNull().WithMessage("Open time is required.")
                    .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("Time must be from 0:00 to 23:59.");

                RuleFor(x => x.CloseTime)
                    .NotEmpty().WithMessage("Close time is required.")
                    .NotNull().WithMessage("Close time is required.")
                    .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$").WithMessage("Time must be from 0:00 to 23:59.");
            });
        }
    }
}
