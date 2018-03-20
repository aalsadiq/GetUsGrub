using CSULB.GetUsGrub.Models;
using FluentValidation;
using System;
using DayOfWeek = System.DayOfWeek;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>BusinessHourValidator</c> class.
    /// Defines rules to validate a BusinessHour.
    /// <para>
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @updated: 03/18/2018
    /// </para>
    /// </summary>
    public class BusinessHourValidator : AbstractValidator<IBusinessHour>
    {
        public BusinessHourValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.Day)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.OpenTime)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$");

                RuleFor(x => x.CloseTime)
                    .NotEmpty()
                    .NotNull()
                    .Matches(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$");
            });
        }

        /// <summary>
        /// The CheckIfDayIsDayOfWeek method.
        /// Checks to see if day is in the enum DayOfWeek.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 03/20/2018
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
        /// <para>
        /// @author: Jennifer Nguyen
        /// @update: 03/20/2018
        /// </para>
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