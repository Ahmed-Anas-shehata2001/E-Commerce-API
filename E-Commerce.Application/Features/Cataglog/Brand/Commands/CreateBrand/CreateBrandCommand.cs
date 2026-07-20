using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.CreateBrand;

public sealed record CreateBrandCommand(
    string Name,
    string? Description)
    : IRequest<Guid>;