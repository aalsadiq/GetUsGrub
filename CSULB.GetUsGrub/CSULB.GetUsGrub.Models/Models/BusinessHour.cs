using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHour</c> class.
    /// Defines properties pertaining to a business hour.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/20/2018
    /// </para>
    /// </summary>
    [Table("GetUsGrub.BusinessHour")]
    public class BusinessHour : IBusinessHour, IEntity
    {
        public BusinessHour() { }

        public BusinessHour(string day, string openTime, string closeTime)
        {
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
        }

        [Key]
        public int? Id { get; set; }

        [ForeignKey("RestaurantProfile")]
        public int? RestaurantId { get; set; }

        [Required]
        public string Day { get; set; }

        [Required]
        public string OpenTime { get; set; }

        [Required]
        public string CloseTime { get; set; }

        // Navigation Properties
        public virtual RestaurantProfile RestaurantProfile { get; set; }
    }
}
