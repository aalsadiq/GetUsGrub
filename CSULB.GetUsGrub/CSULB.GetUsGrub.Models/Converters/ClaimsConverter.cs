using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CSULB.GetUsGrub.Models
{
    // TODO: @Jenn Comment this and unit test [-Jenn]
    class ClaimsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ICollection<System.Security.Claims.Claim>);
        }

        public override object ReadJson(JsonReader jsonReader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var claims = new List<Claim>();
            var jsonObject = JObject.Load(jsonReader);

            foreach (var claim in jsonObject)
            {
                var type = (string)jsonObject["Type"];
                var value = (string)jsonObject["Value"];
                var valueType = (string)jsonObject["ValueType"];
                var issuer = (string)jsonObject["Issuer"];
                var originalIssuer = (string)jsonObject["OriginalIssuer"];
                claims.Add(new Claim(type: type, value: value, valueType: valueType, issuer: issuer, originalIssuer: originalIssuer));
            }

            return claims;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
