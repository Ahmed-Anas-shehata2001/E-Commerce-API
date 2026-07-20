using E_Commerce.Domain.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Cataglog.Product.Commands.RemoveDiscount
{
    public sealed record RemoveDiscountCommand(Guid ProductId) : IRequest<Result>;
}
