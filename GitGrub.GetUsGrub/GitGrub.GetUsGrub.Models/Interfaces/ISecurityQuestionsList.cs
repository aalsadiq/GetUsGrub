using GitGrub.GetUsGrub.Models.Models;
using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface ISecurityQuestionsList
    {
        IEnumerable<SecurityQuestion> SecurityQuestionsList { get; set; }
    }
}
