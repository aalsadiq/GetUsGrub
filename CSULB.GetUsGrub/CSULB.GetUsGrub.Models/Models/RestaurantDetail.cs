using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    [System.Serializable]
    public class RestaurantDetail //: IRestaurantDetail
    {
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

        public string Category { get; set; }

        //public IEnumerable<BusinessHour> BusinessSchedule { get; set; } Ask Brian if we need this?
    }
}
