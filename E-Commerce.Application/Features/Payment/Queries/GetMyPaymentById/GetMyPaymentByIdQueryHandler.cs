using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Payment;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.PaymentFeature.Queries.GetMyPaymentById;

public sealed class GetMyPaymentByIdQueryHandler
    : IRequestHandler<GetMyPaymentByIdQuery, Result<PaymentDto>>
{
    private readonly IPaymentReadRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetMyPaymentByIdQueryHandler(
        IPaymentReadRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Result<PaymentDto>> Handle(
        GetMyPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure<PaymentDto>("User is not authenticated.");

        var payment = await _repository.GetCustomerPaymentByIdAsync(
            _currentUser.UserId.Value,
            request.PaymentId,
            cancellationToken);

        if (payment is null)
            return Result.Failure<PaymentDto>("Payment not found.");

        return Result.Success(payment);
    }
}