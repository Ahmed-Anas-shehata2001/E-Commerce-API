using E_Commerce.Application.Features.CategoryFeature.DTO;
using E_Commerce.Domain.Features.CategoryFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.CategoryFeature.Queries
{
    public class GetAllCategoriesHandler
        : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllCategoriesAsync(cancellationToken);
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }
    }
}
