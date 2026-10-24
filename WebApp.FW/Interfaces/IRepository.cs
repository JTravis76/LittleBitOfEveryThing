using System;
using System.Collections.Generic;

namespace WebApp.FW.Interfaces
{
    public interface IRepository : IDisposable
    {
        /*======================================
        * C R U D methods for Entity Framework
        ========================================*/

        /*==================================
        * CREATE
        ===================================*/

        void Create<T>(T entity) where T : class;
        void Create<T>(ICollection<T> entity) where T : class;
        void Create<T>(IEnumerable<T> entity) where T : class;
        void Create<T>(List<T> entity) where T : class;

        /*==================================
        * READ
        ===================================*/

        IEnumerable<TEntity> Read<TEntity>(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null,
            params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes) where TEntity : class;

        IEnumerable<TEntity> ReadAsNoTracking<TEntity>(
            System.Linq.Expressions.Expression<Func<TEntity, bool>> where = null,
            params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includes) where TEntity : class;

        TEntity ReadById<TEntity>(params object[] keyValues) where TEntity : class;

        /*==================================
        * UPDATE
        ===================================*/

        void Update<T>(T entity) where T : class;
        void Update<T>(ICollection<T> entity) where T : class;
        void Update<T>(IEnumerable<T> entity) where T : class;
        void Update<T>(List<T> entity) where T : class;

        /*==================================
        * DELETE
        ===================================*/

        void Delete<T>(T entity) where T : class;
        void Delete<T>(IEnumerable<T> entity) where T : class;
        void Delete<T>(List<T> entity) where T : class;
        void Delete<TEntity>(object id) where TEntity : class;
        void Delete<TEntity>(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null) where TEntity : class;

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        void ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// Returns a System.Data.Entity.DbSet`1 instance for access to entities of the given type in the context and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
        /// <returns></returns>
        System.Data.Entity.DbSet<TEntity> DbSet<TEntity>() where TEntity : class;

        void BeginTransaction(System.Data.IsolationLevel IsolationLevel = System.Data.IsolationLevel.ReadCommitted);
        void Commit();
    }
}
