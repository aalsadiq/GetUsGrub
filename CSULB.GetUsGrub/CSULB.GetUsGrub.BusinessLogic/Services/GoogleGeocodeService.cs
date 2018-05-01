using CSULB.GetUsGrub.Models;
using Newtonsoft.Json;
using System;
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
            var url = GoogleApiConstants.GOOGLE_GEOCODE_URL;

            url += GoogleApiConstants.GOOGLE_GEOCODE_ADDRESS_QUERY;

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
            url += GoogleApiConstants.GOOGLE_KEY_QUERY + key;

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
            try
            {
                // Retrieve key from configuration and build url for the get request.
                var key = ConfigurationManager.AppSettings[GoogleApiConstants.GOOGLE_GEOCODE_API_KEYWORD];
                var url = BuildUrl(address, key);

                // Send get request and parse the response
                var request = new GetRequestService(url);
                var response = await new GoogleBackoffRequest(request).TryExecute();
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<GoogleGeocodingDto>(responseJson);

                if (responseObj.status != GoogleApiConstants.GOOGLE_GEOCODE_STATUS_OK)
                {
                    if (responseObj.status == GoogleApiConstants.GOOGLE_GEOCODE_STATUS_ZERO_RESULTS)
                    {
                        return new ResponseDto<IGeoCoordinates>()
                        {
                            Error = GoogleApiConstants.GOOGLE_GEOCODE_ERROR_INVALID_ADDRESS
                        };
                    }

                    return new ResponseDto<IGeoCoordinates>()
                    {
                        Error = GoogleApiConstants.GOOGLE_GEOCODE_ERROR_GENERAL
                    };
                }

                var result = responseObj.results[0];

                foreach (var addresses in responseObj.results[0].address_components)
                {
                    foreach (var type in addresses.types)
                    {
                        if (type != GoogleApiConstants.GOOGLE_GEOCODE_TYPE_STREET) continue;

                        var lat = result.geometry.location.lat;
                        var lng = result.geometry.location.lng;
                        var geoCoordinates = new GeoCoordinates(lat, lng);

                        return new ResponseDto<IGeoCoordinates>()
                        {
                            Data = geoCoordinates
                        };
                    }
                }

                return new ResponseDto<IGeoCoordinates>()
                {
                    Error = GoogleApiConstants.GOOGLE_GEOCODE_ERROR_INVALID_ADDRESS
                };
            }
            catch
            {
                return new ResponseDto<IGeoCoordinates>()
                {
                    Error = GoogleApiConstants.GOOGLE_GEOCODE_ERROR_GENERAL
                };
            }
        }
    }
}
