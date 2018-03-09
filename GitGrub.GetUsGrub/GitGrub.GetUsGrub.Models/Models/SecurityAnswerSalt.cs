namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityAnswerSalt</c> class.
    /// Defines a property pertaining to a salt for a security answer.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class SecurityAnswerSalt : ISalt
    {
        public string Salt { get; set; }
    }
}
