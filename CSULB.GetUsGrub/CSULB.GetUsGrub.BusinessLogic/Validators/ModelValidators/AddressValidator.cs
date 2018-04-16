using CSULB.GetUsGrub.Models;
using FluentValidation;
using System;

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
                RuleFor(address => address.Street1)
                    .NotEmpty().WithMessage(ValidationErrorMessages.STREET_1_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.STREET_1_REQUIRED);

                RuleFor(address => address.City)
                    .NotEmpty().WithMessage(ValidationErrorMessages.CITY_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.CITY_REQUIRED);

                RuleFor(address => address.State)
                    .NotEmpty().WithMessage(ValidationErrorMessages.STATE_REQUIRED)
                    .NotNull().WithMessage(ValidationErrorMessages.STATE_REQUIRED)
                    .Must(x => Enum.IsDefined(typeof(States), x)).WithMessage(ValidationErrorMessages.NOT_VALID_STATE);

                RuleFor(address => address.Zip)
                    .NotEmpty().WithMessage(ValidationErrorMessages.ZIP_REQUIRED)
                    // Zip code must contain 5 numbers
                    .GreaterThanOrEqualTo(10000).WithMessage(ValidationErrorMessages.ZIP_FORMAT)
                    // Zip code must contain 5 numbers
                    .LessThanOrEqualTo(99999).WithMessage(ValidationErrorMessages.ZIP_FORMAT);
            });
        }

        // TODO: @Brian Need to integrate with Google Maps API for the following [-Jenn]
        // Call Google Maps API to check if the address is a valid address in Los Angeles, CA and Orange County, CA
    }
}