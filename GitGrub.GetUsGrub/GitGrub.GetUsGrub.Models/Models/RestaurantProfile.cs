using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models.Models
{
    class RestaurantProfile : IProfile, IRestaurantProfile
    public class RestaurantProfile : IProfile, IRestaurantProfile
    {
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        public string Category { get; set; }
        public IEnumerable<BusinessHours> BusinessSchedule { get; set; }
        public bool Reservations { get; set; }
        public bool Delivery { get; set; }
        public bool TakeOut { get; set; }
        public bool AcceptCreditCards { get; set; }
        public string Attire { get; set; }
        public bool ServesAlcohol { get; set; }
        public bool OutdoorSeating { get; set; }
        public bool HasTV { get; set; }
        public bool DriveThru { get; set; }
        public bool Caters { get; set; }
        public bool Pet { get; set; }
        public IList<RestaurantMenuItem> Menu { get; set; }
    }
}
