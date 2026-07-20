using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.UnArchiveCategory;

public sealed class UnArchiveCategoryCommandHandler
    : IRequestHandler<UnArchiveCategoryCommand, Result>
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UnArchiveCategoryCommandHandler(
        ICategoryRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UnArchiveCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetCategoryByIdAsync(
            request.CategoryId,
            cancellationToken);

        if (category is null)
            return Result.Failure("Category not found.");

        category.UnArchive();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}