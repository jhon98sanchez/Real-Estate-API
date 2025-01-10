using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Common.Contracts
{
    public interface IGenericRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {

        #region C - Create

        /// <summary>
        /// Method responsible for inserting a record of type T into the database
        /// </summary>
        /// <param name="entity">Entity to be processed</param>
        /// <returns>Processed entity</returns>
        Task<TEntity> Create(TEntity entity);

        #endregion

        #region R - Read

        /// <summary>
        /// Reads all records of the entity
        /// </summary>
        /// <returns>List of entity records</returns>
        Task<List<TEntity>> ReadAll();

        /// <summary>
        /// Filters and lists the records of an entity without pagination.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>List of entity records</returns>

        Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Filters and lists a single record of an entity without pagination.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>The first entity record that matches the criteria or null</returns>

        Task<TEntity> ReadOne(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Reads all records with the ability to include related entities in the database, including pagination.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <param name="include">Function that determines the related entities to include</param>
        /// <param name="orderBy">Function that determines the sorting for pagination</param>
        /// <param name="page">Page number</param>
        /// <param name="size">Page size</param>
        /// <returns>Object containing the query result</returns>

        Task<PagedResult<TEntity>> Read(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int page = 0, int size = 0);

        /// <summary>
        /// Checks for the existence of a record.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>True if a record matching the expression criteria exists; otherwise, false</returns>

        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region U - Update

        /// <summary>
        /// Method that updates the entity with the provided information.
        /// </summary>
        /// <param name="entity">Processed entity</param>
        /// <returns>True/False</returns>

        Task<TEntity> Update(TEntity entity);

        #endregion

        #region D - Delete

        /// <summary>
        /// Method responsible for deleting entities based on a filter.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>true/false</returns>

        Task<bool> Delete(TEntity data);

        #endregion
    }
}
