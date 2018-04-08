// TODO: @Jenn Comment this [-Jenn]
namespace CSULB.GetUsGrub.Models
{
    public class SsoTokenPayload
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public string Application { get; set; }
        public string IssuedAt { get; set; }
    }
}
