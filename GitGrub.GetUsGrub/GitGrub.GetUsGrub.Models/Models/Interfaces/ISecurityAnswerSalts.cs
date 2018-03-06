using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The ISecurityAnswerSalts interface.
    /// A contract with defined property as list of SecurityAnswerSalts.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface ISecurityAnswerSalts
    {
        IList<SecurityAnswerSalt> SecurityAnswerSalts { get; set; }
    }
}
