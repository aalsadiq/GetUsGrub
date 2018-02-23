namespace GitGrub.GetUsGrub.Models.DTOs
{
    class AuthenticationTokenDto : ISalt
    {
        public string TokenHeader { get; set; }
        public string TokenPayload { get; set; }
        public string TokenSignature { get; set; }
        public string Salt { get; set; }
    }

}
