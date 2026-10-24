using EntityGraphQL.Schema;
using WebApp.Api.Database;

namespace WebApp.Api.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGql
    {
        /// <summary>
        /// Build the Schema-First for GraphQL
        /// </summary>
        /// <returns></returns>
        SchemaProvider<DataContext> MakeSchema();
    }
}
