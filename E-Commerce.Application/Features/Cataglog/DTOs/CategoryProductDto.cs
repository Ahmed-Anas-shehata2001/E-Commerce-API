using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Cataglog.Category.DTO
{
    public sealed class CategoryProductDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public decimal Price { get; init; }

        public decimal currPrice { get; init; }

        public int Stock { get; init; }

        public string SKU { get; init; } = default!;

    }
}
