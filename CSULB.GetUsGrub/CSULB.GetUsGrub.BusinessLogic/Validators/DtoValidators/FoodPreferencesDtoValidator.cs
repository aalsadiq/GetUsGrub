using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Defines the rules to validate the Food Preferences DTO
    /// 
    /// @author: Rachel Dang
    /// @updated: 04/24/18
    /// </summary>
    public class FoodPreferencesDtoValidator : AbstractValidator<FoodPreferencesDto>
    {
        public FoodPreferencesDtoValidator()
        {
            RuleSet("EditFoodPreferences", () =>
            {
                RuleFor(foodPreferencesDto => foodPreferencesDto.FoodPreferences)
                    .NotNull().WithMessage(ValidationErrorMessages.INVALID_TOKEN);
            });
        }
    }
}
