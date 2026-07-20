using E_Commerce.Application.Features.Cataglog.Category.DTO;
using E_Commerce.Application.Features.Catalog.Category.Queries.GetCategoryProducts;
using MediatR;

public sealed class GetCategoryProductsQueryHandler
    : IRequestHandler<GetCategoryProductsQuery, List<CategoryProductDto>>
{
    private readonly ICategoryReadRepository _repository;

    public GetCategoryProductsQueryHandler(
        ICategoryReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryProductDto>> Handle(
        GetCategoryProductsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetCategoryProductsAsync(
            request.CategoryId,
            cancellationToken);
    }
}