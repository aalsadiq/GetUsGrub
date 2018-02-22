namespace GitGrub.GetUsGrub.Models.Models
{
    /// <summary>
    /// Business hours class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class BusinessHours
    {
        private string Day { get; set; }
        private int OpenTime { get; set; }
        private int CloseTime { get; set; }
    }
}