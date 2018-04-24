namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>SsoTokenPayloadKeys</c> class.
    /// Contains constants of the keys accepted from an SSO JWT Payload.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class SsoTokenPayloadKeys
    {
        public const string USERNAME = "username";
        public const string PASSWORD = "password";
        public const string ROLE_TYPE = "roletype";
        public const string APPLICATION = "application";
        public const string IAT = "iat";
    }
}
