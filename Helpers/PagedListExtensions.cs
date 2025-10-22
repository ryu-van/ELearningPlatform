using E_learning_platform.DTOs.Responses

using Microsoft.EntityFrameworkCore;

namespace E_learning_platform.Helpers
{
    public static class PagedListExtensions
    {
        public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<T>(items, count, page, pageSize);
        }
    }
}
