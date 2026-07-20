using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.UpdateBrand;

public sealed record UpdateBrandCommand(
    Guid Id,
    string Name,
    string? Description)
    : IRequest<Result>;