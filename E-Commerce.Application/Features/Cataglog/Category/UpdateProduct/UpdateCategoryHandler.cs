using E_Commerce.Domain.Common.Errors;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Category.UpdateProduct
{
    public class UpdateCategoryHandler
      : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.GetCategoryByIdAsync(request.Id, cancellationToken);

            if (category == null)
                return Result.Failure(new Error("Category not found", "Category.NotFound"));

            //category.Name = request.Name;
            category.SetName(request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
