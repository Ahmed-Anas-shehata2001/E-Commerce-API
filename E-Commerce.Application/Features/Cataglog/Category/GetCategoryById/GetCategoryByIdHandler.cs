using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Application.Features.Cataglog.Category.DTO;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.GetCategoryById;

public sealed class GetCategoryByIdHandler
    : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryByIdHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CategoryDto>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetCategoryByIdAsync(
            request.Id,
            cancellationToken);

        if (category is null)
            throw new CategoryNotFoundException(request.Id);

        var dto = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAtUtc = category.CreatedAtUtc,
            UpdatedAtUtc = category.UpdatedAtUtc
        };

        return Result<CategoryDto>.Success(dto);
    }
}