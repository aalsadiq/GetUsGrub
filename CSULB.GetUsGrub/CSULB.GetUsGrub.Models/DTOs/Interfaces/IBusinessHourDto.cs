using System;

namespace CSULB.GetUsGrub.Models
{
    public interface IBusinessHourDto
    {
        string Day { get; }
        string OpenTime { get; }
        string CloseTime { get; }
        DateTime OpenDateTime { get; }
        DateTime CloseDateTime { get; }
    }
}
