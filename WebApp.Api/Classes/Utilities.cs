using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApp.Api.Classes
{
    public static class Utilities
    {
        /// <summary>
        /// Utility that serialize the JSON object using Newtonsoft.Json
        /// </summary>
        /// <param name="data">object to serialize</param>
        /// <returns></returns>
        public static T SerializerJson<T>(T data) where T : class
        {
            string content = Newtonsoft.Json.JsonConvert.SerializeObject(data,
                new Newtonsoft.Json.JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None,
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    {
                        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                    }
                });
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Utility that serialize the JSON object using System.Text.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T SerializerJsonNet<T>(T data) where T : class
        {
            string content = System.Text.Json.JsonSerializer.Serialize(data,
                new System.Text.Json.JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                });
            return System.Text.Json.JsonSerializer.Deserialize<T>(content);
        }

    }
}
