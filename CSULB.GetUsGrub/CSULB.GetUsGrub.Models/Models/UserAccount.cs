namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations?
    /// <summary>
    /// The <c>UserAccount</c> class.
    /// Defines properties pertaining to a user account.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool FirstTimeUser { get; set; }
    }
}
