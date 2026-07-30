using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Common.Models;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.PaymentFeature.Entities;
using MediatR;

public sealed record GetPaymentsQuery(
    int PageNumber = 1,
    int PageSize = 20,
    PaymentStatus? Status = null,
    PaymentMethod? PaymentMethod = null,
    Guid? OrderId = null,
    Guid? CustomerId = null)
    : IRequest<Result<PagedResult<PaymentDto>>>;

