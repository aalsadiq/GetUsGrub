using System.Collections.Generic;
using GitGrub.GetUsGrub.Models.Interfaces;
using GitGrub.GetUsGrub.Models.Models;

namespace GitGrub.GetUsGrub.Models.DTOs
{
    /// <summary>
    /// DTO representing the restaurant profile information that can be edited
    /// </summary>
    public class EditRestaurantProfileDto : IProfile, IRestaurantProfile, IRestaurantDetails
    {
        public string Username { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double PhoneNumber { get; set; }
        public string Category { get; set; }
        public IEnumerable<BusinessHours> BusinessSchedule { get; set; }
        public bool? HasReservations { get; set; }
        public bool? HasDelivery { get; set; }
        public bool? HasTakeOut { get; set; }
        public bool? AcceptCreditCards { get; set; }
        public string Attire { get; set; }
        public bool? ServesAlcohol { get; set; }
        public bool? HasOutdoorSeating { get; set; }
        public bool? HasTv { get; set; }
        public bool? HasDriveThru { get; set; }
        public bool? Caters { get; set; }
        public bool? AllowsPets { get; set; }
        public List<RestaurantMenu> Menus { get; set; }
    }
}
