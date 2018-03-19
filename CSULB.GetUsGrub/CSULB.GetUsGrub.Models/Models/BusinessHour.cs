using System.ComponentModel.DataAnnotations;


namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Business hour class
    /// 
    /// @author: Andrew Kao
    /// @updated: 3/18/18
    /// </summary>
    public class BusinessHour : IBusinessHour
    {
        [Required]
        public string Day { get; set; }

        [Required]
        public int OpenTime { get; set; }

        [Required]
        public int CloseTime { get; set; }

        [Required]
        public string TimeZone { get; set; }
    }
}
