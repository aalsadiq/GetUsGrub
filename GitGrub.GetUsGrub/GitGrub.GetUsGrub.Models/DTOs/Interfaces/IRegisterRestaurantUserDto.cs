using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public interface IRegisterRestaurantUserDto : IRegisterUserDto
    {
        [Required]
        RestaurantAccount RestaurantAccount { get; set; }
    }
}
