using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    public interface IRestaurantAccount : IAddress, IBusinessHours
    {
        [Required]
        string PhoneNumber { get; set; }
    }
}
