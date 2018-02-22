
﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    [Table("GetUsGrub.RestaurantProfiles")]
    public class RestaurantProfile : IProfile, IRestaurantProfile, IRestaurantDetail

        public string ProfilePicture { get; set; }

        public string Category { get; set; }

        [Column(TypeName = "text")]
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



        public IList<IRestaurantMenu> Menu { get; set; }