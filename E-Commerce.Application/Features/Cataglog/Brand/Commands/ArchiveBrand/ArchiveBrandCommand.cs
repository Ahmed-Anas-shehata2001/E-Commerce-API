using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.ArchiveBrand;

public sealed record ArchiveBrandCommand(Guid BrandId)
    : IRequest<Result>;