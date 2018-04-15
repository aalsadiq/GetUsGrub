using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>BusinessHour</c> class.
    /// Defines properties pertaining to a business hour.
    /// <para>
    /// @author: Jennifer Nguyen, Andrew Kao
    /// @updated: 4/5/2018
    /// </para>
    /// </summary>

    [Table("GetUsGrub.BusinessHour")]
    public class BusinessHour : IBusinessHour, IEntity
    {
        // Automatic Properties
        [Key]
        public int? Id { get; set; }
        [ForeignKey("RestaurantProfile")]
        public int? RestaurantId { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public DateTime OpenTime { get; set; }
        [Required]
        public DateTime CloseTime { get; set; }
        public Flag Flag { get; set; }

        public virtual RestaurantProfile RestaurantProfile { get; set; }

        // Constructors
        public BusinessHour() { }

        public BusinessHour(string day, DateTime openTime, DateTime closeTime)
        {
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
        }

        // For getting
        public BusinessHour(int? id, string day, DateTime openTime, DateTime closeTime)
        {
            Id = id;
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
        }

        // For editing
        public BusinessHour(int? id, string day, DateTime openTime, DateTime closeTime, Flag flag)
        {
            Id = id;
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
            Flag = flag;
        }

        public BusinessHour(int? id, int? restaurantId, string day, DateTime openTime, DateTime closeTime)
        {
            Id = id;
            RestaurantId = restaurantId;
            Day = day;
            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}