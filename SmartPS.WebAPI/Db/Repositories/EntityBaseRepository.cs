using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using SmartPS.WebAPI.Db.Abstract;

namespace SmartPS.WebAPI.Db.Repositories
{
    public class EntityBaseRepository<T, T1> : IEntityBaseRepository<T, T1>
        where T : class, IEntityBase<T1>, new() 
        where T1 : IEquatable<T1>
    {
        private ApplicationDbContext _dbContext;

        #region Properties
        public EntityBaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        public virtual int Count()
        {
            return _dbContext.Set<T>().Count();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }
        public virtual IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }
        public virtual IEnumerable<T> GetAllWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate);
        }

        public T GetSingleWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }
        public T GetSingleWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            _dbContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _dbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }
        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = _dbContext.Set<T>().Where(predicate);

            foreach(var entity in entities)
            {
                _dbContext.Entry<T>(entity).State = EntityState.Deleted;
            }
        }

        public virtual void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
