namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Business hours class
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    public class BusinessHours
    {
        public string Day { get; set; }

        public int OpenTime { get; set; }

        public int CloseTime { get; set; }
    }
}