using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Defines the rules to validate the Food Preference object
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/14/18
    /// </summary>
    public class FoodPreferenceValidator : AbstractValidator<FoodPreference>
    {
        public FoodPreferenceValidator()
        {
            RuleFor(foodPreference => foodPreference.Preference)
                .NotEmpty()
                .NotNull();
        }
    }
}
