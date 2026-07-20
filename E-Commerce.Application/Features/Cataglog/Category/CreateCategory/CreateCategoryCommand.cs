using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name,
    string? Description)
    : IRequest<Guid>;