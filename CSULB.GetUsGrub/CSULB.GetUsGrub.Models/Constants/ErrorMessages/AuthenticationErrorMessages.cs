namespace CSULB.GetUsGrub.Models
{
    public class AuthenticationErrorMessages
    {
        // Error Message for Incorrect Username and Password
        public static string USERNAME_PASSWORD_ERROR = "Your username and password do not match.";

        // Error Message for Locked Account
        public static string LOCKED_ACCOUNT = "Your account has been locked due to too many login attempts. Please try again in 20 minutes.";

        // Error Message User Inactive
        public static string INACTIVE_USER = "Your account is disabled.";

        // Error Message
    }
}
