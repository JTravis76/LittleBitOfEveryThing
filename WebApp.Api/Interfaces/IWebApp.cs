using Microsoft.EntityFrameworkCore;
using WebApp.Api.Database;

namespace WebApp.Api.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebApp
    {
        /// <summary>
        /// Set the EF Tracking state for a list of entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list">collection of entity to track</param>
        /// <param name="context">database context</param>
        /// <param name="state">state to set entity</param>
        void SetDatabaseTracking<TEntity>(List<TEntity> list, DataContext context, EntityState state) where TEntity : class;

    }
}
