using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    interface IRestaurantProfile
    {
        string Category { get; set; }
        IEnumerable<BusinessHours> BusinessSchedule { get; set; }
        bool HasReservations { get; set; }
        bool HasDelivery { get; set; }
        bool HasTakeOut { get; set; }
        bool AcceptCreditCards { get; set; }
        string Attire { get; set; }
        bool ServesAlcohol { get; set; }
        bool HasOutdoorSeating { get; set; }
        bool HasTv{ get; set; }
        bool HasDriveThru { get; set; }
        bool Caters { get; set; }
        bool AllowsPets { get; set; }
        List<RestaurantMenuItem> Menu { get; set; }
    }
}