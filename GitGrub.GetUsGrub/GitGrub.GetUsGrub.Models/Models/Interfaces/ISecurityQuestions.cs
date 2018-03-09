using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The ISecurityQuestions interface.
    /// A contract with defined property as a list of SecurityQuestions.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface ISecurityQuestions
    {
        IList<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
