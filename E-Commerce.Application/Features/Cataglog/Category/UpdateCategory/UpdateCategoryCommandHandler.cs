using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Catalog.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(
        ICategoryRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetCategoryByIdAsync(
            request.Id,
            cancellationToken);

        if (category is null)
            return Result.Failure("Category not found.");

        var exists = await _repository.CategoryNameExistsAsync(
            request.Name,
            cancellationToken);

        if (exists &&
            !string.Equals(category.Name, request.Name, StringComparison.OrdinalIgnoreCase))
        {
            return Result.Failure("Category name already exists.");
        }

        category.Update(
            request.Name,
            request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}