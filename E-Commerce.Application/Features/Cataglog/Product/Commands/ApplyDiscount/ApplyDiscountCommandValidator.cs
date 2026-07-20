using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Cataglog.Product.Commands.ApplyDiscount
{
    public sealed class ApplyDiscountCommandValidator : AbstractValidator<ApplyDiscountCommand>
    {
        public ApplyDiscountCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Percentage).InclusiveBetween(0.01m, 100m);
            RuleFor(x => x.EndDateUtc).GreaterThan(x => x.StartDateUtc);
        }
    }
}
