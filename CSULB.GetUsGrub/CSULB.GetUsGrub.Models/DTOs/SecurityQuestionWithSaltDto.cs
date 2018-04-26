namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestionWithSaltDto</c> class.
    /// Defines properties pertaining to a data transfer object for a security question salt.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/25/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionWithSaltDto
    {
        public int Question;
        public string Answer;
        public string Salt;
    }
}
