using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.UnArchiveBrand;

public sealed class UnArchiveBrandCommandHandler
    : IRequestHandler<UnArchiveBrandCommand, Result>
{
    private readonly IBrandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UnArchiveBrandCommandHandler(
        IBrandRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UnArchiveBrandCommand request,
        CancellationToken cancellationToken)
    {
        var brand = await _repository.GetBrandByIdAsync(
            request.BrandId,
            cancellationToken);

        if (brand is null)
            return Result.Failure("Brand not found.");

        brand.UnArchive();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}