using E_Commerce.Application.Common.Contracts.Payments;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.RefundPayment;

public sealed class RefundPaymentCommandHandler
    : IRequestHandler<RefundPaymentCommand, Result>
{
    private readonly IPaymentService _paymentService;

    public RefundPaymentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<Result> Handle(
        RefundPaymentCommand request,
        CancellationToken cancellationToken)
    {
        await _paymentService.RefundPaymentAsync(
            request.PaymentId,
            cancellationToken);

        return Result.Success();
    }
}