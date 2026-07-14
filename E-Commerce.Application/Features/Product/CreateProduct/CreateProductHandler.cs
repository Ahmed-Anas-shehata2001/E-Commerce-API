using E_Commerce.Domain.Features.CategoryFeature.Interfaces;
using E_Commerce.Domain.Features.ProductFeature.Entities;
using E_Commerce.Domain.Features.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Product.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>  // request type is CreateProductCommand, response type is Guid (the Id of the created product)
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateProductHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 🔴 IMPORTANT: Validate Category exists (Application responsibility)
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);

            if (category == null)
                //throw new Exception("Category not found");
                throw new KeyNotFoundException($"Category with Id {request.CategoryId} not found");

            // ✅ Create domain entity

            E_Commerce.Domain.Features.ProductFeature.Entities.Product product = new E_Commerce.Domain.Features.ProductFeature.Entities.Product(
                request.Name,
                request.Description,
                request.Price,
                request.Stock,
                request.CategoryId
            );

            // ✅ Save
            await _productRepository.AddProductAsync(product);

            return product.Id;
        }
    }
}


