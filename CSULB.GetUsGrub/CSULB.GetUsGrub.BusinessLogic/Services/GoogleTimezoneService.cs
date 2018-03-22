using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using CSULB.GetUsGrub.Models;

namespace CSULB.GetUsGrub.BusinessLogic
{
    public class GoogleTimezoneService : ITimezoneServiceAsync
    {
        private static readonly HttpClient client = new HttpClient();

        private string BuildUrl(IGeoCoordinates coordinates, string key, int timestamp)
        {
            var url = "https://maps.googleapis.com/maps/api/timezone/json?location=";
            url += $"{coordinates.Latitude},{coordinates.Longitude}";
            url += $"&timestamp={timestamp}";
            url += $"&key={key}";

            return url;
        }

        public async Task<int?> GetOffsetAsync(IGeoCoordinates coordinates)
        {
            try
            {
                var baseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                var timeStamp = (int)DateTime.Now.Subtract(baseTime).TotalSeconds;

                var key = ConfigurationManager.AppSettings["GoogleGeocodingApi"];
                var url = BuildUrl(coordinates, key, timeStamp);

                var responseJson = await client.GetStringAsync(url);
                var responseObj = JObject.Parse(responseJson);

                var status = (string)responseObj.SelectToken("status");

                if (!status.Equals("OK"))
                {
                    throw new Exception(status);
                }

                var offset = (int)responseObj.SelectToken("rawOffset");
                var dstOffset = (int)responseObj.SelectToken("dstOffset");

                return offset + dstOffset;
            }
            // If API Key not found, return null
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}