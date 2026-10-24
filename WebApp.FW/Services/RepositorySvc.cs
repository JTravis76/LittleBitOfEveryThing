using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApp.FW.Interfaces;

namespace WebApp.FW.Services
{
    public class RepositorySvc : IRepository
    {
        private readonly DbContext _dbContext = null;
        private System.Data.Entity.DbContextTransaction _transaction = null;


        public RepositorySvc() 
        {
            //Change to match your Data Context here.
            _dbContext = new DataContext.WebAppContext();

            _dbContext.Configuration.ProxyCreationEnabled = false;
            _dbContext.Configuration.LazyLoadingEnabled = false;

            //_dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        /*==================================
         * CREATE
         ===================================*/

        /// <summary>
        /// Add a new record to data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to create</param>
        public void Create<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        /// <summary>
        /// Add a collection of records to data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model collections to create</param>
        public void Create<T>(ICollection<T> entity) where T : class
        {
            foreach (var e in entity)
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Added;
        }

        /// <summary>
        /// Add a collection of records to data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model collections to create</param>
        public void Create<T>(IEnumerable<T> entity) where T : class
        {
            foreach (var e in entity)
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Added;
        }

        /// <summary>
        /// Add a collection of records to data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model collections to create</param>
        public void Create<T>(List<T> entity) where T : class
        {
            foreach (var e in entity)
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Added;
        }


        /*==================================
        * READ
        ===================================*/

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity">Business Object Type Class</typeparam>
        /// <param name="where"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Read<TEntity>(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null,
            params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes
            ) where TEntity : class
        {
            IQueryable<TEntity> query;

            if (where != null)
                //this will send WHERE clause to the SQL server
                query = _dbContext.Set<TEntity>().Where(where).AsQueryable();
            else
                query = _dbContext.Set<TEntity>().AsQueryable();


            //applies the eager-loading expressions
            foreach (var include in includes)
                query = query.Include(include);

            //includes
            //    .Aggregate<System.Linq.Expressions.Expression<Func<TEntity, Object>>, 
            //      IQueryable <TEntity>>(query, (current, expression) => current.Include(expression));

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity">Business Object Type Class</typeparam>
        /// <param name="where"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> ReadAsNoTracking<TEntity>(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null,
            params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes
            ) where TEntity : class
        {
            IQueryable<TEntity> query;

            if (where != null)
                //this will send WHERE clause to the SQL server
                query = _dbContext.Set<TEntity>().Where(where).AsQueryable().AsNoTracking();
            else
                query = _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();

            //applies the eager-loading expressions
            foreach (var include in includes)
                query = query.Include(include);

            return query;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity">Business Object Type Class</typeparam>
        /// <param name="keyValues">The values of the primary key to be found</param>
        /// <returns></returns>
        public virtual TEntity ReadById<TEntity>(params object[] keyValues) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(keyValues);
        }


        /*==================================
        * UPDATE
        ===================================*/

        /// <summary>
        /// Modified the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to modify</param>
        public void Update<T>(T entity) where T : class
        {
            //entity.ClearChildEntities();
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Modified the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model collection to modify</param>
        public void Update<T>(ICollection<T> entity) where T : class
        {
            foreach (var e in entity)
            {
                // e.ClearChildEntities();
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Modified;
            }
        }

        /// <summary>
        /// Modified the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model collection to modify</param>
        public void Update<T>(IEnumerable<T> entity) where T : class
        {
            foreach (var e in entity)
            {
                //e.ClearChildEntities();
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Modified;
            }
        }

        /// <summary>
        /// Modified the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model collection to modify</param>
        public void Update<T>(List<T> entity) where T : class
        {
            foreach (var e in entity)
            {
                //e.ClearChildEntities();
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Modified;
            }
        }


        /*==================================
        * DELETE
        ===================================*/

        /// <summary>
        /// Delete the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(T entity) where T : class
        {
            //entity.ClearChildEntities();
            _dbContext.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        /// <summary>
        /// Delete the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(ICollection<T> entity) where T : class
        {
            var entities = entity.ToList();
            foreach (var e in entities)
            {
                //e.ClearChildEntities();
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
        }

        /// <summary>
        /// Delete the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(IEnumerable<T> entity) where T : class
        {
            foreach (var e in entity)
            {
                //e.ClearChildEntities();
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
        }

        /// <summary>
        /// Delete the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="entity">Entity Model to delete</param>
        public void Delete<T>(List<T> entity) where T : class
        {
            foreach (var e in entity)
            {
                //e.ClearChildEntities();
                _dbContext.Entry(e).State = System.Data.Entity.EntityState.Deleted;
            }
        }

        /// <summary>
        /// Delete the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="id">ID [Primary Key] of Entity Model to delete</param>
        public void Delete<T>(object id) where T : class
        {
            T entityToDel = _dbContext.Set<T>().Find(id);
            _dbContext.Entry(entityToDel).State = System.Data.Entity.EntityState.Deleted;
        }

        /// <summary>
        /// Delete the existing data context model
        /// </summary>
        /// <typeparam name="T">Business Object type class</typeparam>
        /// <param name="filter">Lamba Where expression</param>
        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> filter = null) where T : class
        {
            IQueryable<T> entities = _dbContext.Set<T>().AsQueryable();

            if (filter != null)
                entities = entities.Where(filter);

            if (entities != null)
            {
                foreach (var e in entities)
                {
                    Delete<T>(e);
                }
            }
        }


        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        public void ExecuteSqlCommand(string sql, params object[] parameters)
        {
            _dbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        /// <summary>
        /// Returns a System.Data.Entity.DbSet`1 instance for access to entities of the given type in the context and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns></returns>
        public virtual DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }


        /// <summary>
        /// To open a transaction scope
        /// </summary>
        /// <param name="IsolationLevel"></param>
        public void BeginTransaction(System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            _transaction = _dbContext.Database.BeginTransaction(IsolationLevel);
        }

        /// <summary>
        /// Save changes to database. Commit Transaction scope (if opened), and rollback transaction (if opened and fails)
        /// </summary>
        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();

                _transaction?.Commit();
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    if (_transaction != null)
            //        _transaction.Rollback();

            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        System.Diagnostics.Debug.Print("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            System.Diagnostics.Debug.Print("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;
            //}
            catch (Exception e)
            {
                _transaction?.Rollback();

                throw e;
            }

        }

        /// <summary>
        /// Dispose both the Data Context and Transaction scope
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
            _transaction?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}