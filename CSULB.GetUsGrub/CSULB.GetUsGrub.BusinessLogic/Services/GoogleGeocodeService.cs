using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Implementation of IGeocodeService using Google Map's Geocoding API.
    /// </summary>
    public class GoogleGeocodeService : IGeocodeServiceAsync
    {
        // Single HttpClient used to share connection for all requests.
        private static readonly HttpClient client = new HttpClient();

        private string BuildUrl(IAddress address, string key)
        {
            var url = "https://maps.googleapis.com/maps/api/geocode/json?address=";
            url += $"{address.Street1} {address.Street2}, {address.City}, {address.State} {address.Zip}";
            url.Replace(' ', '+');
            url += $"&key={key}";

            return url;
        }

        public async Task<IGeoCoordinates> GeocodeAddressAsync(IAddress address)
        {
            try
            {
                var key = ConfigurationManager.AppSettings.Get("GoogleGeocodeKey");
                var url = BuildUrl(address, key);
                var responseJson = await client.GetStringAsync(url);
                var jObj = JObject.Parse(responseJson);
                return null;
            }
            // If API Key is not found, throw exception
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}
