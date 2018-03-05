using System.Collections.Generic;
using System.Security.Claims;

namespace GitGrub.GetUsGrub.Models
{
    public interface IRegisterUserDto
    {
        UserAccount UserAccount { get; set; }

        PasswordSalt PasswordSalt { get; set; }

        IList<SecurityQuestion> SecurityQuestions { get; set; }

        IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }

        ICollection<Claim> Claims { get; set; }
    }
}