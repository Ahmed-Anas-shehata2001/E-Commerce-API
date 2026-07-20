using E_Commerce.Application.Features.Catalog.DTOs;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Queries.GetBrandById;

public sealed class GetBrandByIdQueryHandler
    : IRequestHandler<GetBrandByIdQuery, Result<BrandDto>>
{
    private readonly IBrandRepository _repository;

    public GetBrandByIdQueryHandler(
        IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<BrandDto>> Handle(
        GetBrandByIdQuery request,
        CancellationToken cancellationToken)
    {
        var brand = await _repository.GetBrandByIdAsync(
            request.Id,
            cancellationToken);

        if (brand is null)
            return Result<BrandDto>.Failure("Brand not found.");

        return Result<BrandDto>.Success(
            new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description,
                CreatedAtUTC = brand.CreatedAtUtc,
                UpdateAtUTC = brand.UpdatedAtUtc
            });
    }
}