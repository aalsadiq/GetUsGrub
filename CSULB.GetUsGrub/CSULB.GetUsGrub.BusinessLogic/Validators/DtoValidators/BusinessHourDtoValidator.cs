using CSULB.GetUsGrub.Models;
using FluentValidation;
using System;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>BusinessHourDtoValidator</c> class.
    /// Defines rules to validate a BusinessHourDto.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class BusinessHourDtoValidator : AbstractValidator<BusinessHourDto>
    {
        public BusinessHourDtoValidator()
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

        /// <summary>
        /// The CheckIfDayIsDayOfWeek method.
        /// Checks to see if day is in the enum DayOfWeek.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 03/13/2018
        /// </para>
        /// </summary>
        /// <param name="day"></param>
        /// <returns>A boolean</returns>
        public ResponseDto<bool> CheckIfDayIsDayOfWeek(string day)
        {
            // True if day is in DayOfWeek enum
            return new ResponseDto<bool>()
            {
                Data = Enum.IsDefined(typeof(DayOfWeek), day)
            };
        }

        /// <summary>
        /// The CheckIfOpenTimeIsBeforeCloseTime method.
        /// Checks that the opening time is before the closing time.
        /// </summary>
        /// <param name="openTime"></param>
        /// <param name="closeTime"></param>
        /// <returns>A boolean</returns>
        public ResponseDto<bool> CheckIfOpenTimeIsBeforeCloseTime(string openTime, string closeTime)
        {
            var openingTime = TimeSpan.Parse(openTime);
            var closingTime = TimeSpan.Parse(closeTime);
            // -1 means openingTime < closingTime, 0 means openingTime == closingTime, and 1 means openingTime > closingTime
            var compare = TimeSpan.Compare(openingTime, closingTime);
            return new ResponseDto<bool>()
            {
                Data = compare == -1
            };
        }
    }
}
