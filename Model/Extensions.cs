using System.Linq;

namespace Model
{
    public static class Extensions
    {
        public static PagedResult<TEntity> ExecutePagingResult<TEntity>(this IQueryable<TEntity> items, int page, int pageSize)
        {
            var model = items.Skip((page - 1) * pageSize).Take(pageSize);
            return new PagedResult<TEntity> {
                Items = model,
                TotalCount = items.Count()
            };
        }
    }
}
