using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Data.Entity;
using Netotik.Services.Abstract;
using Netotik.Data.Context;
using Netotik.Common;
using Netotik.Data;


namespace Netotik.Services.Implement
{
    /// <summary>
    /// Base class for all SQL based service classes
    /// </summary>
    /// <typeparam name="T">The domain object type</typeparam>
    /// <typeparam name="TU">The database object type</typeparam>
    public class BaseService<T> : IBaseService<T>
        where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal IDbSet<T> dbSet;
        public BaseService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Set<T>();
        }


        /// <summary>
        /// Returns the object with the primary key specifies or the default for the type
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T SingleOrDefault(object primaryKey)
        {
            var dbResult = dbSet.Find(primaryKey);
            return dbResult;
        }

        public bool Exists(object primaryKey)
        {
            return dbSet.Find(primaryKey) == null ? false : true;
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Remove(T entity)
        {
            if (_unitOfWork.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public IQueryable<T> All()
        {
            return dbSet.AsQueryable();
        }


        public IEnumerable<T> Where(Func<T, bool> expression)
        {
            return dbSet.Where(expression);
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }


    }
}
