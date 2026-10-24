using Microsoft.EntityFrameworkCore;
using WebApp.Api.Database;

namespace WebApp.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class WebAppSvc : Interfaces.IWebApp
    {
        /// <summary>
        /// Set the EF Tracking state for a list of entities
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list">collection of entity to track</param>
        /// <param name="context">database context</param>
        /// <param name="state">state to set entity</param>
        public void SetDatabaseTracking<TEntity>(List<TEntity> list, DataContext context, EntityState state) where TEntity : class
        {
            list.ForEach(x => context.Entry(x).State = state);
        }


    }
}
