namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantSelectionErrorMessages</c> class.
    /// Contains constants for error messages pertaining to the user management process.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class UserManagementErrorMessages
    {
        public const string USER_EXISTS = "Username is already used.";

        public const string EMPTY_USERNAME_OR_DISPLAYNAME = "Invalid: Empty new username or displayname.";
    }
}
