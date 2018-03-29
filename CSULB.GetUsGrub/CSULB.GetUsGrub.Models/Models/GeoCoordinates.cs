namespace CSULB.GetUsGrub.Models
{
    // TODO: @Brian Please comment this [-Jenn]
    public class GeoCoordinates : IGeoCoordinates
    {
        public GeoCoordinates() { }

        public GeoCoordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
