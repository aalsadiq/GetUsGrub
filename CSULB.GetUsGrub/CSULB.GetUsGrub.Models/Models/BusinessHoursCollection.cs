using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
namespace CSULB.GetUsGrub.Models
{
    public class BusinessHoursCollection : Collection<BusinessHour>
    {
        public void Add(ICollection<BusinessHour> businessHours)
        {
            foreach(var businessHour in businessHours)
            {
                Add(businessHour);
            }
        }

        public string Serialized
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(this); }
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                var jData = Newtonsoft.Json.JsonConvert.DeserializeObject<Collection<BusinessHour>>(value);
            }
        }
    }
}
