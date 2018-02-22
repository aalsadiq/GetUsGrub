using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models.Interfaces
{
    public interface ISecurityQuestionsList
    {
        IEnumerable<ISecurityQuestion> SecurityQuestionsList { get; set; }
    }
}
