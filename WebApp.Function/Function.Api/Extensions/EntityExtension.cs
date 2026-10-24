using WebAppFunction.Api.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppFunction.Api.Extensions
{
  public static class EntityExtensions
  {
    /// <summary>
    /// Returns a deep copy of the object
    /// </summary>
    /// <typeparam name="T">Type of object being copied</typeparam>
    /// <param name="source">Instance to copy</param>
    /// <returns>Copied object</returns>
    public static T Clone<T>(this T source)
    {
      if (source == null)
        return source;

      var content = Newtonsoft.Json.JsonConvert.SerializeObject(source,
          new Newtonsoft.Json.JsonSerializerSettings()
          {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None
          });

      return JsonConvert.DeserializeObject<T>(content);
    }

    /// <summary>
    /// Sets the Identity inserts before saving to database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    public static void IndentityInsert<T>(this DbContext context)
    {
      var entityType = context.Model.FindEntityType(typeof(T));
      context.Database.OpenConnection();
      try
      {
        context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} ON");
        context.SaveChanges();
        context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} OFF");
      }
      finally
      {
        context.Database.CloseConnection();
      }
    }

    /// <summary>
    /// Sets the Identity inserts before saving to database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    public static async Task IndentityInsertAsync<T>(this DbContext context)
    {
      var entityType = context.Model.FindEntityType(typeof(T));

      context.Database.OpenConnection();
      try
      {
        await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} ON");
        context.SaveChanges();
        await context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} OFF");
      }
      finally
      {
        context.Database.CloseConnection();
      }
    }
  }
}