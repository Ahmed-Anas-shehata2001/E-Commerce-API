

namespace E_Commerce.Application.Common.Models;

    public sealed class PagedResult<T>
    {
        // Generic wrapper for paginated list responses (users, roles, permissions, etc.)

        public IReadOnlyList<T> Items { get; init; } = new List<T>();
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages => PageSize == 0 ? 0 : (int)System.Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }




