using E_Commerce.Application.Common.Contracts.Payments;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.CancelPayment;

public sealed class CancelPaymentCommandHandler
    : IRequestHandler<CancelPaymentCommand, Result>
{
    private readonly IPaymentService _paymentService;

    public CancelPaymentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<Result> Handle(
        CancelPaymentCommand request,
        CancellationToken cancellationToken)
    {
        await _paymentService.CancelPaymentAsync(
            request.PaymentId,
            cancellationToken);

        return Result.Success();
    }
}