using Microsoft.EntityFrameworkCore;

namespace WebApp.Api.Services
{
    public class EFRepository
    {
        private readonly DbContext? _dbContext = null;
        // TODO: private readonly System.Data.Entity.DbContextTransaction _transaction = null;

        public EFRepository()
        {
            _dbContext = new Database.DataContext();
        }

        #region (C)reate

        /// <summary>
        /// Add a new entity model to data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to create</param>
        public void Create<T>(T entity) where T : class
        {
            _dbContext!.Entry(entity).State = EntityState.Added;
        }

        /// <summary>
        /// Add a list of new entity models to data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entities">Entity Models to create</param>
        public void Create<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Added;
        }

        /// <summary>
        /// Add a list of new entity models to data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entities">Entity Models to create</param>
        public void Create<T>(ICollection<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Added;
        }

        /// <summary>
        /// Add a list of new entity models to data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entities">Entity Models to create</param>
        public void Create<T>(List<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Added;
        }

        #endregion

        #region (R)ead

        public virtual IEnumerable<TEntity> Read<TEntity>(
            System.Linq.Expressions.Expression<Func<TEntity, bool>>? filter = null,
            params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes
        ) where TEntity : class
        {
            IQueryable<TEntity> query;

            if (filter == null)
                query = _dbContext!.Set<TEntity>().AsQueryable();
            else
                query = _dbContext!.Set<TEntity>().Where(filter).AsQueryable();

            //applies the eager-loading expressions
            foreach (var include in includes)
                query = query.Include(include);

            //includes
            //    .Aggregate<System.Linq.Expressions.Expression<Func<TEntity, Object>>, IQueryable<TEntity>>
            //    (query, (current, expression) => current.Include(expression));

            if (includes.Length == 0)
                return _dbContext.Set<TEntity>().ToList();
            else
                return query.ToList();
        }

        #endregion

        #region (U)pdate

        /// <summary>
        /// Modified the existing entity model from data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to modify</param>
        public void Update<T>(T entity) where T : class
        {
            _dbContext!.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Modified a list of entity modelss from data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Models to modify</param>
        public void Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Modified;
        }

        /// <summary>
        /// Modified a list of entity modelss from data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Models to modify</param>
        public void Update<T>(List<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Modified;
        }

        #endregion

        #region (D)elete

        /// <summary>
        /// Delete the existing entity model from data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(T entity) where T : class
        {
            _dbContext!.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Delete a list of entity models from data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Deleted;
        }

        /// <summary>
        /// Delete a list of entity models from data context
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(List<T> entities) where T : class
        {
            foreach (var e in entities)
                _dbContext!.Entry(e).State = EntityState.Deleted;
        }

        /// <summary>
        /// Delete the existing entity model from data context
        /// </summary>
        /// <typeparam name="TEntity">Business Object type class</typeparam>
        /// <param name="id">ID [Primary Key] of Entity Model to delete</param>
        public void Delete<TEntity>(object id) where TEntity : class
        {
            TEntity? entityToDel = _dbContext!.Set<TEntity>().Find(id);
            if (entityToDel != null)
                _dbContext!.Entry(entityToDel).State = EntityState.Deleted;
        }

        #endregion



        /// <summary>
        /// To open a transaction scope
        /// </summary>
        /// <param name="IsolationLevel"></param>
        public void BeginTransaction(System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            // TODO: _transaction = _dbContext!.Database.BeginTransaction(IsolationLevel);
        }

        public void Commit()
        {
            try
            {
                _dbContext!.SaveChanges();
                // TODO: _transaction?.Commit();
            }
            catch (Exception)
            {
                // TODO: _transaction?.Rollback();
                throw;
            }

        }

        /// <summary>
        /// Dispose both the Data Context and Transaction scope
        /// </summary>
        public void Dispose()
        {
            // TODO: _transaction?.Dispose();
            _dbContext?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
