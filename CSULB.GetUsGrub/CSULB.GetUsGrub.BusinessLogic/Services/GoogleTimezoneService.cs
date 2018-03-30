using CSULB.GetUsGrub.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Threading.Tasks;
// TODO: @Brian Time Zone is two words. Please change the class name and its interface to reflect this [-Jenn]
namespace CSULB.GetUsGrub.BusinessLogic
{
    /// <summary>
    /// Service for accessing Google's Timezone API to calculate a location's offset from UTC.
    /// 
    /// @Author: Brian Fann
    /// @Last Updated: 3/22/18
    /// </summary>
    public class GoogleTimeZoneService : ITimeZoneServiceAsync
    {
        private string BuildUrl(IGeoCoordinates coordinates, string key, int timestamp)
        {
            var url = "https://maps.googleapis.com/maps/api/timezone/json?";
            url += $"location={coordinates.Latitude},{coordinates.Longitude}";
            url += $"&timestamp={timestamp}";
            url += $"&key={key}";

            return url;
        }

        /// <summary>
        /// Calculates offset from UTC from geocoordinates using Google's Timezone API.
        /// </summary>
        /// <param name="coordinates">Coordinates of location to check time zone.</param>
        /// <returns>Offset of time (in seconds) from UTC.</returns>
        public async Task<ResponseDto<int>> GetOffsetAsync(IGeoCoordinates coordinates)
        {
            try
            {
                // Get current date in seconds to determine if its DST for Google's API.
                var baseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                var timeStamp = (int)DateTime.Now.Subtract(baseTime).TotalSeconds;

                // Retrieve key from configuration and build url for get request.
                var key = ConfigurationManager.AppSettings["GoogleTimezoneApi"];
                var url = BuildUrl(coordinates, key, timeStamp);

                // Send get request and parse response
                var responseJson = await new GoogleBackoffGetRequest(url, "OVER_QUERY_LIMIT").TryExecute();
                var responseObj = JObject.Parse(responseJson);

                // Retrieve status code from response
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

                    return new ResponseDto<int>()
                    {
                        Error = status
                    };
                }

                // Retrieve offsets from response and return the sum of the offsets.
                var offset = (int)responseObj.SelectToken("rawOffset");
                var dstOffset = (int)responseObj.SelectToken("dstOffset");

                return new ResponseDto<int>()
                {
                    Data = offset + dstOffset
                };
            }
            // If API Key not found, throw error.
            catch (ConfigurationErrorsException)
            {
                throw new Exception("Google Timezone API Key not found.");
            }
        }
    }
}