using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CSULB.GetUsGrub.Models
{
    public class ImageUploadDto
    {
        // Automatic Properties
        [Required]
        public string Username { get; set; }
       // public File Image { get; set; }
    }
}
