using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IRegisterUserDto interface.
    /// A contract with defined properties for the RegisterUserDto class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2017
    /// </para>
    /// </summary>
    public interface IRegisterUserDto
    {
        UserAccount UserAccount { get; set; }

        PasswordSalt PasswordSalt { get; set; }

        IList<SecurityQuestion> SecurityQuestions { get; set; }

        IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }

        Claims Claims { get; set; }
    }
}