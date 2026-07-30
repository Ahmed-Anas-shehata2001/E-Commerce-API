using E_Commerce.Application.Common.Contracts.Payments.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.CreatePayment;

public sealed record CreatePaymentCommand(
    Guid OrderId)
    : IRequest<Result<CreatePaymentResult>>;