using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface ISecurityQuestionsList
    {
        IEnumerable<ISecurityQuestion> SecurityQuestionsList { get; set; }
    }
}
