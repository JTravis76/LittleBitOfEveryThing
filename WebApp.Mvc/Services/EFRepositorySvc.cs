using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApp.Mvc.DataContext;

namespace WebApp.Mvc.Services
{
    public class EFRepositorySvc
    {
//        private DbContext _dbContext = null;
//        private System.Data.Entity.DbContextTransaction _transaction = null;

//        /// <summary>
//        /// Start a new Unit of Work with Data Context open
//        /// </summary>
//        /// <param name="ProxyCreationEnabled">False by default</param>
//        public EFRepositorySvc(bool ProxyCreationEnabled = false)
//        {
//            //Update here to match you Data Context!!!
//            _dbContext = new WikiContext();

//            _dbContext.Configuration.ProxyCreationEnabled = ProxyCreationEnabled;
//            _dbContext.Configuration.LazyLoadingEnabled = false;
//        }


//        #region Read Commands

//        public virtual IEnumerable<TEntity> Read<TEntity>(
//            System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null,
//            params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes
//            ) where TEntity : class
//        {
//            IQueryable<TEntity> query;

//            if (filter == null)
//                query = _dbContext.Set<TEntity>().AsQueryable();
//            else
//                query = _dbContext.Set<TEntity>().Where(filter).AsQueryable();

//            //applies the eager-loading expressions
//            foreach (var include in includes)
//                query = query.Include(include);

//            //includes
//            //    .Aggregate<System.Linq.Expressions.Expression<Func<TEntity, Object>>, IQueryable<TEntity>>
//            //    (query, (current, expression) => current.Include(expression));

//            if (includes.Length == 0)
//                return _dbContext.Set<TEntity>().ToList();
//            else
//                return query.ToList();
//        }

//        #endregion

//        #region Create Commands

//        /// <summary>
//        /// Add a new entity model to data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Model to create</param>
//        public void Create<T>(T entity) where T : class
//        {
//            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
//        }

//        /// <summary>
//        /// Add a list of new entity models to data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entities">Entity Models to create</param>
//        public void Create<T>(IEnumerable<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Added;
//        }

//        /// <summary>
//        /// Add a list of new entity models to data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entities">Entity Models to create</param>
//        public void Create<T>(ICollection<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Added;
//        }

//        /// <summary>
//        /// Add a list of new entity models to data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entities">Entity Models to create</param>
//        public void Create<T>(List<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Added;
//        }

//        #endregion

//        #region Update Commands

//        /// <summary>
//        /// Modified the existing entity model from data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Model to modify</param>
//        public void Update<T>(T entity) where T : class
//        {
//            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
//        }

//        /// <summary>
//        /// Modified a list of entity modelss from data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Models to modify</param>
//        public void Update<T>(IEnumerable<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = EntityState.Modified;
//        }

//        /// <summary>
//        /// Modified a list of entity modelss from data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Models to modify</param>
//        public void Update<T>(List<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = EntityState.Modified;
//        }

//        #endregion

//        #region Delete Commands

//        /// <summary>
//        /// Delete the existing entity model from data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Model to delete</param>
//        public void Delete<T>(T entity) where T : class
//        {
//            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
//        }

//        /// <summary>
//        /// Delete a list of entity models from data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Model to delete</param>
//        public void Delete<T>(IEnumerable<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Deleted;
//        }

//        /// <summary>
//        /// Delete a list of entity models from data context
//        /// </summary>
//        /// <typeparam name="T">Business Object type class</typeparam>
//        /// <param name="entity">Entity Model to delete</param>
//        public void Delete<T>(List<T> entities) where T : class
//        {
//            foreach (var e in entities)
//                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Deleted;
//        }

//        /// <summary>
//        /// Delete the existing entity model from data context
//        /// </summary>
//        /// <typeparam name="TEntity">Business Object type class</typeparam>
//        /// <param name="id">ID [Primary Key] of Entity Model to delete</param>
//        public void Delete<TEntity>(object id) where TEntity : class
//        {
//            TEntity entityToDel = _dbContext.Set<TEntity>().Find(id);
//            _dbContext.Entry(entityToDel).State = System.Data.Entity.EntityState.Deleted;
//        }

//        #endregion



//        /// <summary>
//        /// To open a transaction scope
//        /// </summary>
//        /// <param name="IsolationLevel"></param>
//        public void BeginTransaction(System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadCommitted)
//        {
//            _transaction = _dbContext.Database.BeginTransaction(IsolationLevel);
//        }

//        /// <summary>
//        /// Save changes to database. Commit Transaction scope (when opened), and rollback transaction (when opened and fails)
//        /// </summary>
//        public void Commit()
//        {
//            try
//            {
//                _dbContext.SaveChanges();

//                if (_transaction != null)
//                    _transaction.Commit();
//            }
//            catch (Exception e)
//            {
//                if (_transaction != null)
//                    _transaction.Rollback();
//                throw e;
//            }

//        }

//        /// <summary>
//        /// Dispose both the Data Context and Transaction scope
//        /// </summary>
//        public void Dispose()
//        {
//            if (_dbContext != null)
//                _dbContext.Dispose();
//            if (_transaction != null)
//                _transaction.Dispose();

//            GC.SuppressFinalize(this);
//        }

    }
}