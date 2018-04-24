namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>ValidationErrorMessages</c> class.
    /// Contains validation error messages for the project.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class ValidationErrorMessages
    {
        // Validation error messages for BusinessHour
        public const string BUSINESS_DAY_REQUIRED = "Business day is required.";
        public const string OPEN_TIME_REQUIRED = "Open time is required.";
        public const string CLOSE_TIME_REQUIRED = "Close time is required.";
        public const string MILITARY_TIME_FORMAT = "Time must be from 0:00 to 23:59.";

        // Validation error messages for RestaurantProfile
        public const string PHONE_NUMBER_REQUIRED = "Phone number is required.";
        public const string PHONE_NUMBER_FORMAT = "Phone number must be in (XXX)XXX-XXXX format.";

        // Validation error messages for Address
        public const string ADDRESS_REQUIRED = "Address is required.";
        public const string STREET_1_REQUIRED = "Address needs a street1.";
        public const string CITY_REQUIRED = "City is required.";
        public const string STATE_REQUIRED = "State is required.";
        public const string NOT_VALID_STATE = "State must be a valid state.";
        public const string ZIP_REQUIRED = "Address needs a zip code.";
        public const string ZIP_FORMAT = "Zip code is not a valid format.";

        // Validation error messages for RestaurantDetail
        public const string FOOD_TYPE_REQUIRED = "Food type is required.";
        public const string AVG_FOOD_PRICE_REQUIRED = "Average food price is required.";
        public const string NOT_VALID_AVG_FOOD_PRICE = "Average food price is invalid.";

        // Validation error messages for RestaurantSelectionDto
        public const string DISTANCE_REQUIRED = "Distance is required.";
        public const string NOT_VALID_DISTANCE = "Not a valid distance.";

        // Validation error messages for SecurityQuestion
        public const string SECURITY_QUESTION_REQUIRED = "Must answer 3 security questions.";

        // Validation error messages for UserAccount
        public const string USERNAME_REQUIRED = "Username is required.";
        public const string USERNAME_FORMAT = "Username must not contain spaces and special characters.";
        public const string PASSWORD_REQUIRED = "Password is required.";
        public const string PASSWORD_LENGTH = "Password must be at least 8 characters and less than or equal to 64.";
        public const string PASSWORD_FORMAT = "Password must not be empty or contain spaces.";
        public const string USER_DOES_NOT_EXIST = "User does not exist.";
        public const string INVALID_USERNAME = "Invalid username. Please try again.";

        // Validation error messages for EditUserDto
        public const string NEWUSERNAME_FORMAT = "New username must not contain spaces and special characters.";

        // Validation error messages for UserProfile
        public const string DISPLAY_NAME_REQUIRED = "Display name is required.";

        // Validation error messages for a Token
        public const string INVALID_TOKEN = "Token is not valid.";

        // Validation error messages for Images
        public const string INVALID_IMAGE_TYPE = "Invalid image type.";
        public const string INVALID_IMAGE_SIZE = "Invalid image size.";
    }
}
