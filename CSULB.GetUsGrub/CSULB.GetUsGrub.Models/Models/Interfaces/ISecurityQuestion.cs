namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The ISecurityQuestion interface.
    /// A contract with defined properties for the SecurityQuestion class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public interface ISecurityQuestion
    {
        int Question { get; set; }
        string Answer { get; set; }
    }
}
