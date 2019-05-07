using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Utility.Error.Application.Interfaces;

namespace Utility.Error.Persistence.Repositories
{
    /// <summary>
    /// Repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        // Protected Methods.
        #region ProtectedMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        protected Repository(DbContext context)
        {
            _context = context;
        }

        #endregion

        // Interface Implementation.
        #region InterfaceImplementation

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// GetAll.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        /// <summary>
        /// Search For.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        /// <summary>
        /// Search For.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SearchForRange(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            return _context.Set<TEntity>().Where(predicate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Search View For.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int CountItems(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).Count();
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// AddRange.
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// RemoveRange.
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        #endregion
    }
}
