using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    public interface ISecurityQuestionsList
    {
        IEnumerable<ISecurityQuestion> SecurityQuestionsList { get; set; }
    }
}
