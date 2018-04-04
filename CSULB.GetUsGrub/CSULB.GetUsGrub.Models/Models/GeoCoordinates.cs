namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Please comment this [-Jenn]
    public class GeoCoordinates : IGeoCoordinates
    {
        // TODO: @Brian Is it okay if we change this to double? so I can use it in my gateway? [-Jenn]
        // Automatic Properties
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Constructors
        public GeoCoordinates() { }

        public GeoCoordinates(double? latitude, double? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
