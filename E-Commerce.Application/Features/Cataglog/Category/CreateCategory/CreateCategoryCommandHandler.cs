using E_Commerce.Application.Common.Exceptions;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository
            .CategoryNameExistsAsync(request.Name, cancellationToken);

        if (exists)
            throw new CategoryAlreadyExistsException(request.Name);

        var category = new E_Commerce.Domain.Features.Catalog.CategoryFeature.Entities.Category(
            request.Name,
            request.Description);

        await _categoryRepository.AddCategoryAsync(
            category,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}