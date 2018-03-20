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
        string Day { get; }

        string OpenTime { get; }

        string CloseTime { get; }
    }
}