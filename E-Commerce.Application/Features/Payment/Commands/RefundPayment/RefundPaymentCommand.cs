using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.RefundPayment;

public sealed record RefundPaymentCommand(
    Guid PaymentId)
    : IRequest<Result>;