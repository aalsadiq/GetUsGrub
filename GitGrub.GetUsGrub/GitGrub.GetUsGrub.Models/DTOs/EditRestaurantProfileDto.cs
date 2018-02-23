using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// DTO encapsulating username and editable restaurant profile information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/22/18
    /// </summary>
    public class EditRestaurantProfileDto : IProfile, IRestaurantProfile, IRestaurantDetail
    {
        public string Username { get; }
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
        public IList<IRestaurantMenu> Menus { get; set; }
    }
}
