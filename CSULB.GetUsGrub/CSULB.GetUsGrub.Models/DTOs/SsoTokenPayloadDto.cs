namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SsoTokenPayloadDto</c> class.
    /// Contains properties associated with the keys in a SSO Token Payload.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class SsoTokenPayloadDto
    {
        // Automatic properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public string Application { get; set; }
        public string IssuedAt { get; set; }
    }
}
