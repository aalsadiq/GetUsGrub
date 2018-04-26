namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>ResetPasswordValidationTypes</c> class.
    /// Contains constant values to identify different validations for resetting a password in the reset password pre/post validation strategies.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/22/2018
    /// </para>
    /// </summary>
    public class ResetPasswordValidationTypes
    {
        public const string GetSecurityQuestionsValidation = "GetSecurityQuestionsValidation";
        public const string ConfirmSecurityQuestionAnswersValidation = "ConfirmSecurityAnswersValidation";
        public const string UpdatePasswordValidation = "UpdatePasswordValidation";
    }
}
