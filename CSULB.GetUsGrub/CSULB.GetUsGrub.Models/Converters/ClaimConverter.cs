using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this and unit test [-Jenn]
    class ClaimConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(System.Security.Claims.Claim);
        }

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
