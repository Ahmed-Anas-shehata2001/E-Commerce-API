using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.OrderFeature.Entities;

namespace E_Commerce.Domain.Features.PaymentFeature.Entities;


public enum PaymentMethod
{
    CreditCard = 1,
    DebitCard = 2,
    PayPal = 3,
    Stripe = 4,
    Paymob = 5,
    CashOnDelivery = 6
}
public enum PaymentStatus
{
    Pending = 1,
    Paid = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5
}
public class Payment  : AuditableEntity
{

    // Relationship
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = default!;

    // Business Data
    public decimal Amount { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public PaymentStatus Status { get; private set; }

    // External Gateway
    public string? TransactionId { get; private set; }

    public string? PaymentIntentId { get; private set; }

    public string? Gateway { get; private set; }

    // Audit
    public DateTime? PaidAtUtc { get; private set; }

    public DateTime? RefundedAtUtc { get; private set; }

    private Payment()
    {
    }

    public Payment(
        Guid orderId,
        decimal amount,
        PaymentMethod paymentMethod,
        string gateway)
    {
        Id = Guid.NewGuid();

        OrderId = orderId;
        Amount = amount;

        PaymentMethod = paymentMethod;
        Gateway = gateway;

        Status = PaymentStatus.Pending;

        CreatedAtUtc = DateTime.UtcNow;
    }

    public void MarkAsPaid(string transactionId, string paymentIntentId)
    {
        Status = PaymentStatus.Paid;
        TransactionId = transactionId;
        PaymentIntentId = paymentIntentId;
        PaidAtUtc = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
    }

    public void MarkAsCancelled()
    {
        Status = PaymentStatus.Cancelled;
    }

    public void MarkAsRefunded()
    {
        Status = PaymentStatus.Refunded;
        RefundedAtUtc = DateTime.UtcNow;
    }
}