using E_Commerce.Application.Features.CategoryFeature.DTO;
using E_Commerce.Application.Features.ProductFeature.DTO;
using E_Commerce.Domain.Features.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.CategoryFeature.Queries
{
    public class GetCategoryByIdHandler
      : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _repository.GetCategoryByIdAsync(request.Id, cancellationToken);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                Products = category.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId,
                    CategoryName = category.Name,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                }).ToList()
            };
        }
    }
}
