using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sharedo.Api
{
    public static class Json
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string Serialise(object obj)
        {
            return JsonConvert.SerializeObject(obj, Json.JsonSerializerSettings);
        }

        public static T Deserialise<T>(string body)
        {
            return JsonConvert.DeserializeObject<T>(body, Json.JsonSerializerSettings);
        }
    }
}
