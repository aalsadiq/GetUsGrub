using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>RestaurantSelectionDto</c> class.
    /// Defines properties pertaining to a data transfer object for a restaurant selection.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    public class RestaurantSelectionDto
    {
        // Automatic Properties
        [Required]
        public string City { get; set; }
        [Required]
        public string State  { get; set; }
        [Required]
        public string FoodType { get; set; }
        [Required]
        public double DistanceInMiles { get; set; }
        [Required]
        public int AvgFoodPrice { get; set; }
        public string Username { get; set; }
        public DateTime CurrentUtcDateTime { get; set; }
        public DayOfWeek CurrentLocalDayOfWeek { get; set; }
        public GeoCoordinates ClientUserGeoCoordinates { get; set; }
        public DbGeography Location
        {
            get
            {
                int srid = 4326;
                string wkt = $"POINT({ClientUserGeoCoordinates.Longitude} {ClientUserGeoCoordinates.Latitude})";

                return DbGeography.PointFromText(wkt, srid);
            }
            set
            {
                ClientUserGeoCoordinates.Latitude = value.Latitude ?? 0;
                ClientUserGeoCoordinates.Latitude = value.Longitude ?? 0;
            }
        }

        public ICollection<FoodPreference> FoodPreferences { get; set; }

        // Constructors
        public RestaurantSelectionDto() { }
        public RestaurantSelectionDto(string city, string state, string foodType, int distanceInMiles, int avgFoodPrice)
        {
            City = city;
            State = state;
            FoodType = foodType;
            DistanceInMiles = distanceInMiles;
            AvgFoodPrice = avgFoodPrice;
        }
    }
}