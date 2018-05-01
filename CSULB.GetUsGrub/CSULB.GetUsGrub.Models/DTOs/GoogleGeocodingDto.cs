using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    public class GoogleGeocodingAddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class GoogleGeocodingLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GoogleGeocodingNortheast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GoogleGeocodingSouthwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GoogleGeocodingViewport
    {
        public GoogleGeocodingNortheast northeast { get; set; }
        public GoogleGeocodingSouthwest southwest { get; set; }
    }

    public class GoogleGeocodingGeometry
    {
        public GoogleGeocodingLocation location { get; set; }
        public string location_type { get; set; }
        public GoogleGeocodingViewport viewport { get; set; }
    }

    public class GoogleGeocodingResult
    {
        public List<GoogleGeocodingAddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public GoogleGeocodingGeometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class GoogleGeocodingDto
    {
        public List<GoogleGeocodingResult> results { get; set; }
        public string status { get; set; }
    }
}
