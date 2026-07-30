using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.CancelPayment;

public sealed record CancelPaymentCommand(
    Guid PaymentId)
    : IRequest<Result>;