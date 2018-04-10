using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>ClaimConverter</c> class.
    /// Converts a Json formatted Claim to a System.Security.Claims.Claim object.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/09/2018
    /// </para>
    /// </summary>
    class ClaimConverter : JsonConverter
    {
        /// <summary>
        /// The CanConvert method.
        /// Checks to see if the object is of System.Security.Claims.Claim type.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns>bool</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(System.Security.Claims.Claim);
        }

        /// <summary>
        /// The ReadJson method.
        /// Takes a Json object and converts it to a System.Security.Claims.Claim object.
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 04/09/2018
        /// </para>
        /// </summary>
        /// <param name="jsonReader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns>Claim object</returns>
        public override object ReadJson(JsonReader jsonReader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(jsonReader);

            var type = (string)jsonObject["Type"];
            var value = (string)jsonObject["Value"];
            var valueType = (string)jsonObject["ValueType"];
            var issuer = (string)jsonObject["Issuer"];
            var originalIssuer = (string)jsonObject["OriginalIssuer"];

            return new Claim(type: type, value: value, valueType: valueType, issuer: issuer, originalIssuer: originalIssuer);
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
