namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The ISecurityQuestion interface.
    /// A contract with defined properties for the SecurityQuestion class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public interface ISecurityQuestion
    {
        int QuestionType { get; set; }
        string QuestionAnswer { get; set; }
    }
}
