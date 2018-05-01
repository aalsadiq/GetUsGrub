using System;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The IBusinessHour interface.
    /// A contract with defined properties for the BusinessHour class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/10/2018
    /// </para>
    /// </summary>
    public interface IBusinessHour
    {
        string Day { get; }
        DateTime OpenTime { get; }
        DateTime CloseTime { get; }
    }
}
