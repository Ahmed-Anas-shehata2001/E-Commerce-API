using E_Commerce.Domain.Features.PaymentFeature.Entities;
public record PaymentDto(
    Guid Id,
    Guid OrderId,
    decimal Amount,
    PaymentStatus Status,
    PaymentMethod Method,
    string? Gateway,
    string? TransactionId,
    DateTime CreatedAtUtc,
    DateTime? PaidAtUtc);