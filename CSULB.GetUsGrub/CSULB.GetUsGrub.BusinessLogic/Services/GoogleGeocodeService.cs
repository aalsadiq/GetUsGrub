using System.Threading.Tasks;
using CSULB.GetUsGrub.Models;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class GoogleGeocodeService : IGeocodeServiceAsync
    {
        private static readonly HttpClient client = new HttpClient();

        private string BuildUrl(IAddress address, string key)
        {
            var url = "https://maps.googleapis.com/maps/api/geocode/json?address=";
            url += $"{address.Street1} {address.Street2}, {address.City}, {address.State} {address.Zip}";
            url.Replace(' ', '+');
            url += $"&key={key}";

            return url;
        }

        public async Task<IGeoCoordinates> GeocodeAsync(IAddress address)
        {
            try
            {
                var key = ConfigurationManager.AppSettings["GoogleGeocodingApi"];
                var url = BuildUrl(address, key);

                var responseJson = await client.GetStringAsync(url);
                var responseObj = JObject.Parse(responseJson);

                var status = (string)responseObj.SelectToken("status");

                if (!status.Equals("OK"))
                {
                    throw new System.Exception(status);
                }

                var lat = (float)responseObj.SelectToken("results[0].geometry.location.lat");
                var lng = (float)responseObj.SelectToken("results[0].geometry.location.lng");

                var coordinates = new GeoCoordinates()
                {
                    Latitude = lat,
                    Longitude = lng
                };

                return coordinates;
            }
            // If API Key not found, return null
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}
