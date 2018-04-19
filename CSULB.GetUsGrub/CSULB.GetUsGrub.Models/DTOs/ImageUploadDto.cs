using System.ComponentModel.DataAnnotations;
using System.Web;

namespace CSULB.GetUsGrub.Models
{
    public class ImageUploadDto
    {
        // Automatic Properties
        [Required]
        public string Username { get; set; }

    }
}
