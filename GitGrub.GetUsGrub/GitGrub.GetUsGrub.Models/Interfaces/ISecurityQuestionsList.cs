using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface ISecurityQuestionsList
    {
        IEnumerable<SecurityQuestion> SecurityQuestionsList { get; set; }
    }
}
