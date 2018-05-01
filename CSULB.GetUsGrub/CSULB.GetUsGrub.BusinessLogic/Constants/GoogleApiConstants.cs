namespace CSULB.GetUsGrub.BusinessLogic
{
    public class GoogleApiConstants
    {
        public const string GOOGLE_GEOCODE_API_KEYWORD = "GoogleGeocodingApi";
        public const string GOOGLE_GEOCODE_URL = "https://maps.googleapis.com/maps/api/geocode/json?";
        public const string GOOGLE_GEOCODE_ADDRESS_QUERY = "address=";
        public const string GOOGLE_GEOCODE_TOKEN_STATUS = "status";
        public const string GOOGLE_GEOCODE_TYPE_STREET = "route";
        public const string GOOGLE_GEOCODE_STATUS_OK = "OK";
        public const string GOOGLE_GEOCODE_STATUS_ZERO_RESULTS = "ZERO_RESULTS";
        public const string GOOGLE_GEOCODE_ERROR_INVALID_ADDRESS = "Invalid address.";
        public const string GOOGLE_GEOCODE_ERROR_GENERAL = "An unexpected error has occurred.";

        public const string GOOGLE_TIMEZONE_API_KEYWORD = "GoogleTimeZoneApi";
        public const string GOOGLE_TIMEZONE_URL = "https://maps.googleapis.com/maps/api/timezone/json?";
        public const string GOOGLE_TIMEZONE_LOCATION_QUERY = "location=";
        public const string GOOGLE_TIMEZONE_TIMESTAMP_QUERY = "&timestamp=";
        public const string GOOGLE_TIMEZONE_STATUS_OK = "OK";
        public const string GOOGLE_TIMEZONE_STATUS_ZERO_RESULTS = "ZERO_RESULTS";
        public const string GOOGLE_TIMEZONE_ERROR_INVALID_ADDRESS = "Invalid address.";
        public const string GOOGLE_TIMEZONE_ERROR_GENERAL = "An unexpected error has occurred.";

        public const string GOOGLE_KEY_QUERY = "&key=";
    }
}
