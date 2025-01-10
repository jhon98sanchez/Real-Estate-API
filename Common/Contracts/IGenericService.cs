using Common.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Contracts
{
    public interface IGenericService<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {

        /// <summary>
        /// Method responsible for inserting a record of type T into the database.
        /// </summary>
        /// <param name="entity">Entity to be processed</param>
        /// <returns>Processed entity</returns>
        Task<ResponseBase<TEntity>> Create(TEntity entity);


        /// <summary>
        /// Filters and lists the records of an entity without pagination.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>List of entity records</returns>
        Task<ResponseBase<List<TEntity>>> Read(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Retrieves an entity that matches the filters.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>List of entity records</returns>
        Task<ResponseBase<TEntity>> ReadOne(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Reads all records with the ability to include related entities in the database, including pagination.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <param name="include">Function that determines the related entities to include</param>
        /// <param name="orderBy">Function that determines the sorting for pagination</param>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        /// <returns>Object containing the query result</returns>
        Task<ResponseBase<PagedResult<TEntity>>> Read(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int page = 0, int size = 0);

        /// <summary>
        /// Checks for the existence of a record.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>True if a record matching the expression criteria exists; otherwise, false</returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);


        /// <summary>
        /// Method that updates the entity with the provided information.
        /// </summary>
        /// <param name="entity">Processed entity</param>
        /// <returns>True/False</returns>

        Task<ResponseBase<TEntity>> Update(TEntity newEntity);

        /// <summary>
        /// Method responsible for deleting entities based on a filter.
        /// </summary>
        /// <param name="expression">Expression representing the entity filter</param>
        /// <returns>true/false</returns>
        Task<ResponseBase<bool>> Delete(TEntity entity);
    }
}
