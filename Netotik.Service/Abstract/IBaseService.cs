using Netotik.Common;
using Netotik.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Netotik.Services.Abstract
{

    public interface IBaseService<T>
    {


        /// <summary>
        /// Retrieve a single item by it's primary key or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        T SingleOrDefault(object primaryKey);

        /// <summary>
        /// Returns all the rows for type T
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();



        /// <summary>
        /// Returns all the rows for type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Where(Func<T, bool> expression);
        /// <summary>
        /// Does this item exist by it's primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        bool Exists(object primaryKey);

        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        void Add(T entity);

        /// <summary>
        /// Updates this entity in the database using it's primary key
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="userId">The user performing the update</param>
        void Update(T entity);

        /// <summary>
        /// Deletes this entry fro the database
        /// ** WARNING - Most items should be marked inactive and Updated, not deleted
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="userId">The user Id who deleted the entity</param>
        /// <returns></returns>
        void Remove(T entity);

        IUnitOfWork UnitOfWork { get; }



    }
}