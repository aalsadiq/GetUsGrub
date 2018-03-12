namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Business hour interface
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public interface IBusinessHour
    {
        string Day { get; set; }

        int OpenTime { get; set; }

        int CloseTime { get; set; }

        string TimeZone { get; set; }
    }
}
