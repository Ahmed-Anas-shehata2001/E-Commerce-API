using E_Commerce.Application.Common.Contracts.Payments;
using E_Commerce.Application.Common.Contracts.Payments.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Commands.CreatePayment;

public sealed class CreatePaymentCommandHandler
    : IRequestHandler<CreatePaymentCommand, Result<CreatePaymentResult>>
{
    private readonly IPaymentService _paymentService;

    public CreatePaymentCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task<Result<CreatePaymentResult>> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _paymentService.CreatePaymentAsync(
            request.OrderId,
            cancellationToken);

        return Result<CreatePaymentResult>.Success(result);
    }
}