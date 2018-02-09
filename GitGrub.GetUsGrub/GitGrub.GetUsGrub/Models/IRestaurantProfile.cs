using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitGrub.GetUsGrub.Models
{
    interface IRestaurantProfile
    {
        string GetCategory();
        IEnumerable<BusinessHour> GetBusinessHours();

        bool GetReservations();
        bool GetDelivery();
        bool GetTakeOut();
        bool GetAcceptCreditCards();
        string GetAttire();
        bool GetServesAlcohol();
        bool GetOutdoorSeating();
        bool GetHasTV();
        bool GetDriveThru();
        bool GetCaters();
        bool GetPets();
        Object GetMenu();
    }
}