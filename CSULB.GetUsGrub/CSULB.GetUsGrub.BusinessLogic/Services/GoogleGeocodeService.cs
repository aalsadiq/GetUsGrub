using CSULB.GetUsGrub.Models;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service for accessing Google's Geocoding API to handle geocoding.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/29/18
    /// </summary>
    public class GoogleGeocodeService : IGeocodeService, IGeocodeServiceAsync
    {
        private string BuildUrl(IAddress address, string key)
        {
            var url = "https://maps.googleapis.com/maps/api/geocode/json?address=";

            if (address.Street1 != null)
            {
                url += $"{address.Street1}";
                if (address.Street2 != null)
                {
                    url += $" {address.Street2}";
                }
                url += ", ";
            }

            url += $"{address.City}, {address.State} {address.Zip}";
            url.Replace(' ', '+');
            url += $"&key={key}";

            return url;
        }

        /// <summary>
        /// Converts an address into geocoordinates using Google's Geocoding API.
        /// This is a synchronous method wrapped around GeocodeAsync().
        /// </summary>
        /// <param name="address">Address to geocode.</param>
        /// <returns>Coordinates of address.</returns>
        public ResponseDto<IGeoCoordinates> Geocode(IAddress address)
        {
            var result = Task.Run(() => GeocodeAsync(address)).Result;

            return result;
        }

        /// <summary>
        /// Converts an address into geocoordinates using Google's Geocoding API.
        /// </summary>
        /// <param name="address">Address to geocode.</param>
        /// <returns>Coordinates of address.</returns>
        public async Task<ResponseDto<IGeoCoordinates>> GeocodeAsync(IAddress address)
        {
            // Retrieve key from configuration and build url for the get request.
            var key = ConfigurationManager.AppSettings["GoogleGeocodingApi"];
            var url = BuildUrl(address, key);

            // Send get request and parse the response
            var request = new GetRequestService(url);
            var response = await new GoogleBackoffRequest(request).TryExecute();
            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObj = JObject.Parse(responseJson);

            // Retrieve status code from the response
            var status = (string)responseObj.SelectToken("status");

            // Exit early if status is not OK.
            if (!status.Equals("OK"))
            {
                if (status.Equals("ZERO_RESULTS"))
                {
                    status = "Invalid Input";
                }
                else
                {
                    status = "Unexpected Error";
                }

                return new ResponseDto<IGeoCoordinates>()
                {
                    Error = status
                };
            }

            // Retrieve latitude and longitude data from response and return coordinates.
            var lat = (float)responseObj.SelectToken("results[0].geometry.location.lat");
            var lng = (float)responseObj.SelectToken("results[0].geometry.location.lng");

            return new ResponseDto<IGeoCoordinates>()
            {
                Data = new GeoCoordinates()
                {
                    Latitude = lat,
                    Longitude = lng
                }
            };
        }
    }
}
