using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cataglog.Category.UpdateProduct
{
    public class UpdateCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public UpdateCategoryCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}