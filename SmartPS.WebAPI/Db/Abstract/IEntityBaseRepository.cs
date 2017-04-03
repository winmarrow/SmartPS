using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartPS.WebAPI.Db.Abstract
{
    public interface IEntityBaseRepository<T, T1> 
        where T : class, IEntityBase<T1>, new() 
        where T1: IEquatable<T1>
    {
        int Count();
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        T GetSingleWhere(Expression<Func<T, bool>> predicate);
        T GetSingleWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
    }
}
