namespace E_Commerce.Application.Common.Contracts.Payments.DTOs;

public sealed record RefundPaymentRequest
(
    string TransactionId,
    decimal Amount
);