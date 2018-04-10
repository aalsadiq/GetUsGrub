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
                RuleFor(businessHourDto => businessHourDto.Day)
                    .NotEmpty().WithMessage(ValidationErrorMessages.BUSINESS_DAY_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.BUSINESS_DAY_REQUIRED);

                RuleFor(businessHourDto => businessHourDto.OpenTime)
                    .NotEmpty().WithMessage(ValidationErrorMessages.OPEN_TIME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.OPEN_TIME_REQUIRED)
                    .Matches(RegularExpressions.MILITARY_TIME_FORMAT).WithMessage(ValidationErrorMessages.MILITARY_TIME_FORMAT);

                RuleFor(businessHourDto => businessHourDto.CloseTime)
                    .NotEmpty().WithMessage(ValidationErrorMessages.CLOSE_TIME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.CLOSE_TIME_REQUIRED)
                    .Matches(RegularExpressions.MILITARY_TIME_FORMAT).WithMessage(ValidationErrorMessages.MILITARY_TIME_FORMAT);
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
                Data = (compare == -1)
            };
        }
    }
}
