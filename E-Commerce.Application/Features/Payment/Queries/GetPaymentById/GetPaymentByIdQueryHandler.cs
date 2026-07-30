using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.PaymentFeature.Interfaces;
using MediatR;

public sealed class GetPaymentByIdQueryHandler
    : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
    private readonly IPaymentRepository _payments;

    public GetPaymentByIdQueryHandler(IPaymentRepository payments)
    {
        _payments = payments;
    }

    public async Task<Result<PaymentDto>> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var payment = await _payments.GetByIdAsync(
            request.PaymentId,
            cancellationToken);

        if (payment is null)
            return Result.Failure<PaymentDto>("Payment not found.");

        return Result.Success(new PaymentDto(
            payment.Id,
            payment.OrderId,
            payment.Amount,
            payment.Status,
            payment.PaymentMethod,
            payment.Gateway,
            payment.TransactionId,
            payment.CreatedAtUtc,
            payment.PaidAtUtc));
    }
}