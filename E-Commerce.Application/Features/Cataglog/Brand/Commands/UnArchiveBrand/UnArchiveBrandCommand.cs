using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.UnArchiveBrand;

public sealed record UnArchiveBrandCommand(Guid BrandId)
    : IRequest<Result>;