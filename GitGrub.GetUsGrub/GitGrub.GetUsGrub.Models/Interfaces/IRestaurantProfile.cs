using System;
using System.Collections.Generic;
using System.Text;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// Interface representing restaurant information
    /// 
    /// Author: Andrew Kao
    /// Last Updated: 2/20/18
    /// </summary>
    interface IRestaurantProfile
    {
        string Street { get; set; }
        string City { get; set; }
        string State { get; set; }
        int ZipCode { get; set; }
        double Longitude { get; set; }
        double Latitude { get; set; }
        double PhoneNumber { get; set; }
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