using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace E_Commerce.Application.Features.Product.CreateProduct
{

    public record CreateProductCommand(string Name,
        string Description,
        decimal Price,
        int Stock,
        Guid CategoryId
    ) : IRequest<Guid>; // response type is Guid (the Id of the created product)
}
