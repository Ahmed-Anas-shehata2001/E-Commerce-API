using E_Commerce.Application.Common.Contracts.Payments;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.VerifyPayment;

public sealed class VerifyPaymentCommandHandler
    : IRequestHandler<VerifyPaymentCommand, Result>
{
    private readonly IPaymentService _paymentService;

    public VerifyPaymentCommandHandler(
        IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<Result> Handle(
        VerifyPaymentCommand request,
        CancellationToken cancellationToken)
    {
        await _paymentService.VerifyPaymentAsync(
            request.TransactionId,
            cancellationToken);

        return Result.Success();
    }
}