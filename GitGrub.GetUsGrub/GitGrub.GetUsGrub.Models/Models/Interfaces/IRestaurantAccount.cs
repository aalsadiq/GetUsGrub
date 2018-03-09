using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IRestaurantAccount interface.
    /// A contract with defined properties for the RestaurantAccount class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface IRestaurantAccount : IAddress, IBusinessHours
    {
        [Required]
        string PhoneNumber { get; set; }
    }
}
