namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RegularExpressions</c> class.
    /// Contains constants of regular expressions used in project.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class RegularExpressions
    {
        // General regular expressions
        public const string STRING_CONTAINS_NO_SPACES = @"^[^\s]+$";

        // Regular expressions for BusinessHour
        public const string MILITARY_TIME_FORMAT = @"^([01]?[0-9]|2[0-3]):[0-5][0-9]$";
        
        // Regular expressions for RestaurantProfile
        public const string PHONE_NUMBER_FORMAT = @"^\([2-9]\d{2}\)\d{3}\-\d{4}$";

        // Regular expressions for UserAccount
        public const string USERNAME_FORMAT = @"^[A-Za-z\d]+$";
    }
}
