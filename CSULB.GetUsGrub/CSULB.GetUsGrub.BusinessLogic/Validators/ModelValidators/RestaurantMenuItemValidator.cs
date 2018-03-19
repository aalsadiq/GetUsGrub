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
                    .NotEmpty().WithMessage("Item name is required.")
                    .NotNull().WithMessage("Item name is required.");

                RuleFor(x => x.ItemPrice)
                    .NotEmpty().WithMessage("Item price is required.")
                    .NotNull().WithMessage("Item price is required.");

                RuleFor(x => x.ItemType)
                    .NotEmpty().WithMessage("Item type is required.")
                    .NotNull().WithMessage("Item type is required.");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Item description is required.")
                    .NotNull().WithMessage("Item description is required.");
            });
        }
    }
}
