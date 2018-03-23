using System.ComponentModel.DataAnnotations;

namespace CSULB.GetUsGrub.Models
{
    public class BusinessHourDto : IBusinessHour
    {
        [Required]
        public string Day { get; set; }

        [Required]
        public string OpenTime { get; set; }

        [Required]
        public string CloseTime { get; set; }
    }
}
