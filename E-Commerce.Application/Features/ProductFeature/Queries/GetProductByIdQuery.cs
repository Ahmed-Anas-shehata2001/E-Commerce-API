using E_Commerce.Application.Features.ProductFeature.DTO;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.ProductFeature.Queries
{
    public class GetProductByIdQuery : IRequest<Result<ProductDto>>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
