using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    public interface ISecurityAnswerSalts
    {
        IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }
    }
}
