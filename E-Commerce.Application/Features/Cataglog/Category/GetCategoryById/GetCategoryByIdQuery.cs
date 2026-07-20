
using E_Commerce.Application.Features.Cataglog.Category.DTO;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id)
    : IRequest<Result<CategoryDto>>;