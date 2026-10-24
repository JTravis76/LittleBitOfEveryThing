using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;

namespace WebAppFunction.Api.Converters
{
  /// <summary>
  /// Converts source type to destination type instead of normal member mapping
  /// </summary>
  /// <typeparam name="EntityType">The type of entity to be converted.</typeparam>
  public class GenericRequestConverter<EntityType> : ITypeConverter<HttpRequest, EntityType>
  {
    /// <summary>
    /// Converts the request stream
    /// </summary>
    /// <param name="request">The <see cref="HttpRequest"/> containing the json stream to be converted.</param>
    /// <returns>An instance of <see cref="EntityType"/></returns>
    private static EntityType Convert(HttpRequest request) =>
       JsonConvert.DeserializeObject<EntityType>(request.ReadAsStringAsync().Result);

    /// <summary>
    /// Performs conversion from source to destination type
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="entity">Destination object</param>
    /// <param name="context">Resolution context</param>
    /// <returns>Destination object as an instance of <see cref="EntityType"/>.</returns>
    public EntityType Convert(HttpRequest source, EntityType entity, ResolutionContext context)
    {
      entity = Convert(source);

          foreach (var property in entity.GetType().GetProperties())
          {
              // Using reflection to get the property info
              var prop = entity.GetType().GetProperty(property.Name);

                var requireds = (System.ComponentModel.DataAnnotations.RequiredAttribute[])prop
                  .GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false);

              if (requireds != null && requireds.Any())
              {
                  if (prop.GetValue(entity) == null)
                      throw new System.Exception(requireds[0].ErrorMessage ?? $"{property.Name} is required");
              }
          }

      return entity;
    }
  }
}
