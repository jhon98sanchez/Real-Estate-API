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
        /// Metodo encargado de insertar un registro del Tipo T en BD
        /// </summary>
        /// <param name="entity">Entidad a procesar</param>
        /// <returns>Entidad procesada</returns>
        Task<TEntity> Create(TEntity entity);

        #endregion

        #region R - Read

        /// <summary>
        /// Lee todos los registros de la entidad
        /// </summary>
        /// <returns>Listado de registros de la entidad</returns>
        Task<List<TEntity>> ReadAll();

        /// <summary>
        /// Filtra y lista los registros de una entidad sin paginar.
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>Listado de registros de la entidad</returns>
        Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Filtra y lista un registro de una entidad sin paginar.
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>El primer registro de la entidad que concuerde con los criterio o null</returns>
        Task<TEntity> ReadOne(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Consulta la existencia de un registro
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>True si existe registro con el cliterio expression de lo contrario false</returns>
        Task<bool> Exists(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region U - Update

        /// <summary>
        /// Metodo que actualiza la entidad con la informacion suministrada
        /// </summary>
        /// <param name="entity">Entidad procesada</param>
        /// <returns>True/False</returns>
        Task<TEntity> Update(TEntity entity);

        #endregion

        #region D - Delete

        /// <summary>
        /// Metodo encargado de eliminar entidades a partir de un filtro
        /// </summary>
        /// <param name="expression">Expresion que representa el filtro de la entidad</param>
        /// <returns>true/false</returns>
        Task<bool> Delete(TEntity data);

        #endregion
    }
}
