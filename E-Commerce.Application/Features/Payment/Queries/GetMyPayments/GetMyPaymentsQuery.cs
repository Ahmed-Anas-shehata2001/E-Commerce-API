using E_Commerce.Application.Common.Models;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.PaymentFeature.Entities;
using MediatR;

public sealed record GetMyPaymentsQuery(
    int PageNumber = 1,
    int PageSize = 20,
    PaymentStatus? Status = null,
    PaymentMethod? PaymentMethod = null)
    : IRequest<Result<PagedResult<PaymentDto>>>;