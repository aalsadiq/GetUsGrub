using System;

namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHour
    {
        string Day { get; }

        DateTime OpenTime { get; }

        DateTime CloseTime { get; }
    }
}
