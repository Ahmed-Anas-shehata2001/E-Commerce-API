using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.UpdateBrand;

public sealed class UpdateBrandCommandHandler
    : IRequestHandler<UpdateBrandCommand, Result>
{
    private readonly IBrandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBrandCommandHandler(
        IBrandRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateBrandCommand request,
        CancellationToken cancellationToken)
    {
        var brand = await _repository.GetBrandByIdAsync(
            request.Id,
            cancellationToken);

        if (brand is null)
            return Result.Failure("Brand not found.");

        var exists = await _repository.BrandNameExistsAsync(
            request.Name,
            cancellationToken);

        if (exists &&
            !string.Equals(
                brand.Name,
                request.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            return Result.Failure("Brand name already exists.");
        }

        brand.Update(
            request.Name,
            request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}