namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SecurityQuestionDto</c> class.
    /// Defines properties pertaining to a data transfer object of a security question.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class SecurityQuestionDto
    {
        // Automatic Properties
        public int Question { get; set; }
        public string Answer { get; set; }
    }
}
