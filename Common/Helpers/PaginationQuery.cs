using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Helpers
{
    public static class PaginationQuery
    {
        private const int PageSizeMax = 100;

        /// <summary>
        /// Method responsible for pagination.
        /// </summary>
        /// <typeparam name="T">Type of entity to process</typeparam>
        /// <param name="query">Query to paginate</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Sorting method</param>
        /// <param name="asList">true/false</param>
        /// <returns>Object containing the pagination result of the defined query</returns>

        public static async Task<PagedResult<T>> Paginate<T>(this IQueryable<T> query, int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool asList = false) where T : class
        {
            if (pageSize > PageSizeMax)
            {
                pageSize = PageSizeMax;
            }

            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = !asList ? query.Count() : query.ToList().Count
            };
            try
            {
                if (page == 0)
                {
                    result.Results = !asList ? await query.ToListAsync() : query.ToList();
                    return result;
                }

                if (orderBy is not null) query = orderBy(query);
                var pageCount = (double)result.RowCount / pageSize;
                result.PageCount = (int)Math.Ceiling(pageCount);

                var skip = (page - 1) * pageSize;
                var sql = query.Skip(skip).Take(pageSize).AsQueryable();
                result.Results = !asList ? await sql.ToListAsync() : sql.ToList();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
