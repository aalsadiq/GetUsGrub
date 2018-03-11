namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Add data annotations? [-Jenn]
    /// <summary>
    /// The <c>PasswordSalt</c> class.
    /// Defines a property of a salt for a password.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public class PasswordSalt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Salt { get; set; }
    }
}
