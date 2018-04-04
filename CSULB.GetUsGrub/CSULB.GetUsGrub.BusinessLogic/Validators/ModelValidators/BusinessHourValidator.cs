using CSULB.GetUsGrub.Models;
using FluentValidation;

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
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.OpenTime)
                    .NotEmpty()
                    .NotNull();

                RuleFor(x => x.CloseTime)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
