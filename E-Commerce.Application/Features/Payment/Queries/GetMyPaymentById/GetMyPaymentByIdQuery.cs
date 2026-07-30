using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Queries.GetMyPaymentById;

public sealed record GetMyPaymentByIdQuery(
    Guid PaymentId)
    : IRequest<Result<PaymentDto>>;