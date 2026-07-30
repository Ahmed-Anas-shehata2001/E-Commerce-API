using E_Commerce.Application.Common.Models;
using E_Commerce.Application.Features.Payment;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Queries.GetPayments;

public sealed class GetPaymentsQueryHandler
    : IRequestHandler<GetPaymentsQuery, Result<PagedResult<PaymentDto>>>
{
    private readonly IPaymentReadRepository _paymentReadRepository;

    public GetPaymentsQueryHandler(
        IPaymentReadRepository paymentReadRepository)
    {
        _paymentReadRepository = paymentReadRepository;
    }

    public async Task<Result<PagedResult<PaymentDto>>> Handle(
        GetPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _paymentReadRepository.GetPaymentsAsync(
            request,
            cancellationToken);

        return Result.Success(result);
    }
}