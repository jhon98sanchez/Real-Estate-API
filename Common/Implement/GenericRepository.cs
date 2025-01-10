using Common.Contracts;
using Common.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Common.Helpers;

namespace Common.Implement
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly IServiceScopeFactory _serviceScope;

        public GenericRepository(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope;
        }

        #region Create

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            await context.Set<TEntity>().AddAsync(entity);
            var affectedRecords = await context.SaveChangesAsync();
            return entity;
        }


        #endregion

        #region Read

        public async Task<List<TEntity>> ReadAll()
        {
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> Read(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                using var scope = _serviceScope.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TContext>();
                var query = context.Set<TEntity>().AsNoTracking();
                return await query.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }

        public async Task<PagedResult<TEntity>> Read(Expression<Func<TEntity, bool>> expression,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                  int page = 0,
                                                  int size = 0)
        {

            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            var query = context.Set<TEntity>().AsNoTracking();
            if (orderBy is not null)
                query = orderBy(query);
            if (include is not null)
                query = include(query);
            query = query.Where(expression).AsQueryable();
            return await query.Paginate(page, size, orderBy);
        }

        public async Task<TEntity> ReadOne(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                using var scope = _serviceScope.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TContext>();
                var query = context.Set<TEntity>().AsNoTracking();
                var result = await query.FirstOrDefaultAsync(expression);
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> expression)
        {
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            var query = context.Set<TEntity>().AsNoTracking();
            return await query.Where(expression).AnyAsync();
        }
        #endregion

        #region Update

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Update(entity);
            var affectedRecords = await context.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Delete

        public virtual async Task<bool> Delete(TEntity data)
        {
            using var scope = _serviceScope.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            context.Remove(data);
            int affectedRecords = await context.SaveChangesAsync();
            return affectedRecords > 0;
        }

        #endregion

    }
}
