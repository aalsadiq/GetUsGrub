namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>GeneralErrorMessages</c> class.
    /// Contains general error constants for the project.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class GeneralErrorMessages
    {
        // Error messages for general errors
        public const string GENERAL_ERROR = "Something went wrong. Please try again later.";

        // Error messages for controllers
        public const string MODEL_STATE_ERROR = "A required input is missing.";

        // Error messages for forbidden errors
        public const string FORBIDDEN_ERROR = "403 Forbidden. You are not authorized to perform this request.";
    }
}
