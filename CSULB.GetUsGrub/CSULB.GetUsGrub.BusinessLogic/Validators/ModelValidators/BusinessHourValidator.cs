using CSULB.GetUsGrub.Models;
using FluentValidation;
using System;
using System.Globalization;
using DayOfWeek = System.DayOfWeek;

// TODO: @Jenn Comment BusinessHourValidator [-Jenn]
// TODO: @Jenn Unit Test the two methods below [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
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

        public bool CheckIfDayIsDayOfWeek(string day)
        {
            // True if day is in DayOfWeek enum
            return Enum.IsDefined(typeof(DayOfWeek), day);
        }

        public bool CheckIfOpenTimeIsBeforeCloseTime(string openTime, string closeTime)
        {
            var openingTime = DateTime.ParseExact(openTime, "HH:mm", CultureInfo.InvariantCulture);
            var closingTime = DateTime.ParseExact(closeTime, "HH:mm", CultureInfo.InvariantCulture);
            // -1 means openingTime < closingTime, 0 means openingTime == closingTime, and 1 means openingTime > closingTime
            var compare = TimeSpan.Compare(openingTime.TimeOfDay, closingTime.TimeOfDay);
            return compare == -1;
        }
    }
}
