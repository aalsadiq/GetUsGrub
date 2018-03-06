namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IUserAccount interface.
    /// A contract with defined properties for the YserAccount class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface IUserAccount
    {
        string Username { get; set; }

        string DisplayName { get; set; }

        string Password { get; set; }

        bool IsActive { get; set; }
    }
}
