using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebAppFunction.Api.Classes
{
  public static class Utilities
  {
    /// <summary>
    /// Converts Rating from Amps to Mega Volt Amperage (MVA)
    /// </summary>
    /// <param name="circuitKv"></param>
    /// <param name="rating"></param>
    /// <returns></returns>
    public static int ConvertToMVA(int circuitKv, int rating)
    {
      return (int)((Math.Sqrt(3) * (double)circuitKv * (double)rating) / 1000);
    }

    /// <summary>
    /// Utility that serialize the JSON object using Newtonsoft.Json
    /// </summary>
    /// <param name="data">object to serialize</param>
    /// <returns></returns>
    public static T SerializerJson<T>(T data) where T : class
    {
      var content = Newtonsoft.Json.JsonConvert.SerializeObject(data,
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
      var content = System.Text.Json.JsonSerializer.Serialize(data,
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
