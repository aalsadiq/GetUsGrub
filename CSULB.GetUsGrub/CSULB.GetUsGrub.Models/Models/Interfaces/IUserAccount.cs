namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The IUserAccount interface.
    /// A contract with defined properties for the UserAccount class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public interface IUserAccount
    {
        string Username { get; }
        string Password { get; }
        bool IsActive { get; }
        bool IsFirstTimeUser { get; }
    }
}
