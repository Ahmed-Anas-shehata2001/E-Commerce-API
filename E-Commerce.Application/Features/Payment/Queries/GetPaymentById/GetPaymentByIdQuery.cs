using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed record GetPaymentByIdQuery(Guid PaymentId)
    : IRequest<Result<PaymentDto>>;