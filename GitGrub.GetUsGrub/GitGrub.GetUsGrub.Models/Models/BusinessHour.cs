using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public class BusinessHour
    {
        // TODO: Validate that it is in military time
        [Required]
        public string Day { get; set; }

        [Required]
        public int OpenTime { get; set; }

        [Required]
        public int CloseTime { get; set; }
    }
}