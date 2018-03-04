using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface ISecurityQuestions
    {
        IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
