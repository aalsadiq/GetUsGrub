namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Business hour interface
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public interface IBusinessHour
    {
        int PublicHourId { get; set; }

        string Day { get; set; }

        string OpenTime { get; set; }

        string CloseTime { get; set; }
    }
}
