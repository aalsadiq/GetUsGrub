namespace GitGrub.GetUsGrub.Models
{
    public interface IRegisterUserDto : IUserAccount, ISecurityQuestions, ISalt
    {}
}