using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class RestaurantMenuItemValidator : AbstractValidator<IMenuItem>
    {
        public RestaurantMenuItemValidator()
        {
            RuleSet("EditProfile", () =>
            {
                RuleFor(x => x.ItemName)
                    .NotEmpty().WithMessage(ValidationErrorMessages.MENU_ITEM_NAME_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.MENU_ITEM_NAME_REQUIRED);

                RuleFor(x => x.ItemPrice)
                    .NotEmpty().WithMessage(ValidationErrorMessages.MENU_ITEM_PRICE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.MENU_ITEM_PRICE_REQUIRED);

                RuleFor(x => x.Tag)
                    .NotEmpty().WithMessage(ValidationErrorMessages.MENU_ITEM_TYPE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.MENU_ITEM_TYPE_REQUIRED);

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage(ValidationErrorMessages.MENU_ITEM_DESCRIPTION_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.MENU_ITEM_DESCRIPTION_REQUIRED);
            });
        }
    }
}
