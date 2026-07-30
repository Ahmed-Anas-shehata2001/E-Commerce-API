using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Models;
using E_Commerce.Application.Features.Payment;
using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed class GetMyPaymentsQueryHandler
    : IRequestHandler<GetMyPaymentsQuery, Result<PagedResult<PaymentDto>>>
{
    private readonly IPaymentReadRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetMyPaymentsQueryHandler(
        IPaymentReadRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<Result<PagedResult<PaymentDto>>> Handle(
        GetMyPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
        {
            return Result.Failure<PagedResult<PaymentDto>>(
                "User is not authenticated.");
        }

        var payments = await _repository.GetCustomerPaymentsAsync(
            _currentUser.UserId.Value,
            request,
            cancellationToken);

        return Result.Success(payments);
    }
}