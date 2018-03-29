using CSULB.GetUsGrub.Models;
using FluentValidation;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// The <c>AddressValidator</c> class.
    /// Defines rules to validate an Address.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/11/2018
    /// </para>
    /// </summary>
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleSet("CreateUser", () =>
            {
                RuleFor(x => x.Street1)
                    .NotEmpty().WithMessage("Address needs a street1.")
                    .NotNull().WithMessage("Address needs a street1.");

                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("Address needs a city.")
                    .NotNull().WithMessage("Address needs a city.");

                RuleFor(x => x.State)
                    .NotEmpty().WithMessage("Address needs a state.")
                    .NotNull().WithMessage("Address needs a state.");

                RuleFor(x => x.Zip)
                    .NotEmpty().WithMessage("Address needs a zip code.")
                    .GreaterThanOrEqualTo(10000).WithMessage("Zip code must contain 5 numbers.")
                    .LessThanOrEqualTo(99999).WithMessage("Zip code must contain 5 numbers.");
            });
        }

        // TODO: @Brian Need to integrate with Google Maps API for the following [-Jenn]
        // Call Google Maps API to check if the address is a valid address in Los Angeles, CA and Orange County, CA

        // TODO: @Jenn Validate enum for states? [-Jenn]
    }
}