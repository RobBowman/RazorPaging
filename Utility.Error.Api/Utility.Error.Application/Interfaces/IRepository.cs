using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Utility.Error.Application.Interfaces
{
    /// <summary>
    /// IRepository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> SearchForRange(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize);
        int CountItems(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}


