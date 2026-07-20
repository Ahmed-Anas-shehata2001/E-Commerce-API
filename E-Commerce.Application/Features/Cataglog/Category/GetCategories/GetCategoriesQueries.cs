using E_Commerce.Application.Features.Cataglog.Category.DTO;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Category.GetCategories;

public sealed record GetCategoriesQuery()
    : IRequest<List<CategoryDto>>;