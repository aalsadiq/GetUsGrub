using FluentValidation;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantProfileValidator</c> class.
    /// Defines rules to validate a RestaurantProfile.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class RestaurantProfileValidator : AbstractValidator<RestaurantProfile>
    {
        public RestaurantProfileValidator()
        {
            RuleFor(x => x.BusinessHours)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .Matches(@"^\([2-9]\d{2}\)\d{3}\-\d{4}$");

            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull();

            // TODO: @Jenn Add regex rule to match Google Map's output type [-Jenn]
            RuleFor(x => x.Longitude)
                .NotEmpty()
                .NotNull();

            // TODO: @Jenn Add regex rule to match Google Map's output type [-Jenn]
            RuleFor(x => x.Latitude)
                .NotEmpty()
                .NotNull();
        }
    }
}
