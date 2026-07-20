using E_Commerce.Application.Features.Cataglog.Category.DTO;
using E_Commerce.Application.Features.Cataglog.Category.GetCategories;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Category.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryRepository _repository;

    public GetCategoriesQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryDto>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _repository.GetCategoriesAsync(cancellationToken);

        return categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAtUtc,
                UpdatedAt = c.UpdatedAtUtc
            })
            .ToList();
    }
}