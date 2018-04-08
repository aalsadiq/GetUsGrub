namespace CSULB.GetUsGrub.Models
{
    public class SsoErrorMessages
    {
        // Error messages for SsoController
        public static string NO_TOKEN_ERROR = "Request does not contain a token.";

        // Error messages for SsoTokenManager
        public static string INVALID_TOKEN_PAYLOAD = "Token payload contains invalid information.";

        // Error messages for SsoTokenPreLogicValidationStrategy
        public static string TOKEN_EXISTS_ERROR = "Token already exists.";

         

    }
}
