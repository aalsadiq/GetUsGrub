using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantMenuValidator : AbstractValidator<IRestaurantMenu>
    {
        public RestaurantMenuValidator()
        {
            RuleSet("EditProfile", () =>
            {
            RuleFor(x => x.MenuName)
                .NotEmpty().WithMessage(ValidationErrorMessages.MENU_REQUIRED)
                .NotNull().WithMessage(ValidationErrorMessages.MENU_REQUIRED);
            });
        }
    }
}
