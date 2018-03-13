namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UserAccountDto</c> class.
    /// Defines properties pertaining to a data transfer object of user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserAccountDto : IUserAccount
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
