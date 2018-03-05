using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public interface IAddress
    {
        [Required]
        Address Address { get; set; }
    }
}
