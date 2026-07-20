

namespace E_Commerce.Application.Features.Cataglog.Category.DTO
{
    public sealed class CategoryDetailsDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public string? Description { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }

        public IReadOnlyList<CategoryProductDto> Products { get; init; }
            = [];
    }
}
