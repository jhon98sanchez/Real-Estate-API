using Common.Contracts;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using Common.Helpers;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.Implement
{
    public class GenericService<TEntity, TContext> : IGenericService<TEntity, TContext>
                where TEntity : class
                where TContext : DbContext
    {
        private readonly IGenericRepository<TEntity, TContext> _repository;

        
        public GenericService(IGenericRepository<TEntity, TContext> repository)
        {
            _repository = repository;
        }


        public virtual async Task<ResponseBase<TEntity>> Create(TEntity entity)
        {
            ResponseBase<TEntity> response = new();
            try
            {
                response.Data = await _repository.Create(entity).ConfigureAwait(true);
                response.Success = true;
                response.Message = "Resource created successfully";
                return response;
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<TEntity>(true, ex, HttpStatusCode.InternalServerError);
            }
        }

        public virtual async Task<ResponseBase<bool>> Delete(TEntity entity)
        {
            ResponseBase<bool> response = new();
            try
            {
                response.Data = await _repository.Delete(entity).ConfigureAwait(true);
                response.Message = "Resources successfully delete";
                return response;
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<bool>(true, ex, HttpStatusCode.InternalServerError);
            }

        }

        
        public virtual async Task<ResponseBase<List<TEntity>>> Read(Expression<Func<TEntity, bool>> expression)
        {

            ResponseBase<List<TEntity>> response = new();
            try
            {
                response.Data = await _repository.Read(expression).ConfigureAwait(true);
                return response;
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<List<TEntity>>(true, ex, HttpStatusCode.InternalServerError);
            }
        }

        public virtual async Task<ResponseBase<PagedResult<TEntity>>> Read(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int page = 0, int size = 0)
        {
            ResponseBase<PagedResult<TEntity>> response = new();
            try
            {
                response.Data = await _repository.Read(expression, include, orderBy, page, size).ConfigureAwait(true);
                return response;
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<PagedResult<TEntity>>(true, ex, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
        {
            return await _repository.Exists(expression).ConfigureAwait(true);
        }

        public virtual async Task<ResponseBase<TEntity>> Update(TEntity newEntity)
        {
            try
            {
                ResponseBase<TEntity> response = new()
                {
                    Data = await _repository.Update(newEntity).ConfigureAwait(true),
                    Success = true,
                    Message = "Resources successfully updated"
                };
                return response;
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<TEntity>(true, ex, HttpStatusCode.InternalServerError);
            }
        }

        public virtual async Task<ResponseBase<TEntity>> ReadOne(Expression<Func<TEntity, bool>> expression)
        {
            ResponseBase<TEntity> response = new();
            try
            {
                response.Data = await _repository.ReadOne(expression);
                if (response.Data is null)
                {
                    response.Message = "Resource not found";
                    response.Code = HttpStatusCode.NotFound;
                }
                return response;
            }
            catch (Exception ex)
            {
                return GenericUtility.ResponseBaseCatch<TEntity>(true, ex, HttpStatusCode.InternalServerError);
            }
        }
    }
}
