using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Wishlist.DTOs;

    public sealed class WishlistDto
    {
        public Guid WishlistId { get; init; }

        public Guid CustomerId { get; init; }

        public List<WishlistItemDto> Items { get; init; } = [];
    }

    public sealed class WishlistItemDto
    {
        public Guid ProductId { get; init; }

        public string ProductName { get; init; } = default!;

        public decimal Price { get; init; }

        public string? ImageUrl { get; init; }
    public bool InStock { get; init; }
}



