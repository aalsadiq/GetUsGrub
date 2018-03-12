using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.Models
{
    public class RestaurantProfileDto : IUserProfile, IRestaurantProfile, IRestaurantDetail
    {
        public string DisplayName { get; set; }

        public string DisplayPicture { get; set; }

        public string RestaurantName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int ZipCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double PhoneNumber { get; set; }

        public IList<IRestaurantMenu> Menus { get; set; }

        public IEnumerable<IBusinessHour> BusinessHours { get; set; }

        public string RestaurantType { get; set; }

        public string Category { get; set; }

        public IEnumerable<IBusinessHour> BusinessSchedule { get; set; }

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
    }
}
