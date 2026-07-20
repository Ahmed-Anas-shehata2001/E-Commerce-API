using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description)
    : IRequest<Result>;