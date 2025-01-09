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
        /// Metodo encargado de insertar un registro del Tipo T en BD
        /// </summary>
        /// <param name="entity">Entidad a procesar</param>
        /// <returns>Entidad procesada</returns>
        Task<ResponseBase<TEntity>> Create(TEntity entity);


        /// <summary>
        /// Lee todos los registros de la entidad
        /// </summary>
        /// <returns>Listado de registros de la entidad</returns>
        Task<ResponseBase<List<TEntity>>> ReadAll();

        /// <summary>
        /// Filtra y lista los registros de una entidad sin paginar.
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>Listado de registros de la entidad</returns>
        Task<ResponseBase<List<TEntity>>> Read(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Obtiene una entidad que concuerde con los filtros.
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>Listado de registros de la entidad</returns>
        Task<ResponseBase<TEntity>> ReadOne(Expression<Func<TEntity, bool>> expression);


        /// <summary>
        /// Consulta la existencia de un registro
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>True si existe registro con el cliterio expression de lo contrario false</returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);


        /// <summary>
        /// Metodo que actualiza la entidad con la informacion suministrada
        /// </summary>
        /// <param name="entity">Entidad procesada</param>
        /// <returns>True/False</returns>
        Task<ResponseBase<TEntity>> Update(TEntity newEntity);


        /// <summary>
        /// Metodo encargado de eliminar entidades a partir de un filtro
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>true/false</returns>
        Task<ResponseBase<bool>> Delete(TEntity entity);


    }
}
