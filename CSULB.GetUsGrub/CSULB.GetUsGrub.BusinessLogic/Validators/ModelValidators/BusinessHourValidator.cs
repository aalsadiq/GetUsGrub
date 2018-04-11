using CSULB.GetUsGrub.Models;
using FluentValidation;

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
                RuleFor(businessHour => businessHour.Day)
                    .NotEmpty()
                    .NotNull();

                RuleFor(businessHour => businessHour.OpenTime)
                    .NotEmpty()
                    .NotNull();

                RuleFor(businessHour => businessHour.CloseTime)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
