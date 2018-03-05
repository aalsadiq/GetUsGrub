using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface ISecurityQuestions
    {
        IList<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
