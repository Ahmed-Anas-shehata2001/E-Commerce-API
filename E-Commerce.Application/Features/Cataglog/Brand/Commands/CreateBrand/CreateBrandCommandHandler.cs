using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Entities;
using E_Commerce.Domain.Features.Catalog.BrandFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Brands.Commands.CreateBrand;

public sealed class CreateBrandCommandHandler
    : IRequestHandler<CreateBrandCommand, Guid>
{
    private readonly IBrandRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBrandCommandHandler(
        IBrandRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateBrandCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.BrandNameExistsAsync(
            request.Name,
            cancellationToken);

        if (exists)
            throw new BrandAlreadyExistsException(request.Name);

        var brand = new Brand(
            request.Name,
            request.Description);

        await _repository.AddBrandAsync(
            brand,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return brand.Id;
    }
}