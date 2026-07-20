
using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrands;

public sealed class GetBrandsQueryHandler
    : IRequestHandler<GetBrandsQuery, List<BrandDto>>
{
    private readonly IBrandRepository _repository;

    public GetBrandsQueryHandler(
        IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BrandDto>> Handle(
        GetBrandsQuery request,
        CancellationToken cancellationToken)
    {
        var brands = await _repository.GetBrandsAsync(
            cancellationToken);

        return brands
            .Select(b => new BrandDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                CreatedAtUTC = b.CreatedAtUtc,
                UpdateAtUTC = b.UpdatedAtUtc
            })
            .ToList();
    }
}