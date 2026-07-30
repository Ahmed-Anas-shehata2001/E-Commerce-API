using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.VerifyPayment;

public sealed record VerifyPaymentCommand(
    string TransactionId)
    : IRequest<Result>;