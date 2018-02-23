using System.Collections.Generic;

namespace GitGrub.GetUsGrub.Models
{
    interface IRestaurantProfile
    {
        string Category { get; set; }
        IEnumerable<BusinessHours> BusinessSchedule { get; set; }
        bool Reservations { get; set; }
        bool Delivery { get; set; }
        bool TakeOut { get; set; }
        bool AcceptCreditCards { get; set; }
        string Attire { get; set; }
        bool ServesAlcohol { get; set; }
        bool OutdoorSeating { get; set; }
        bool HasTV { get; set; }
        bool DriveThru { get; set; }
        bool Caters { get; set; }
        bool Pet { get; set; }
        string PhoneNumber { get; set; }
        IList<RestaurantMenuItem> Menu { get; set; }
    }
}