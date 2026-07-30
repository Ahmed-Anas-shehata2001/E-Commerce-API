using E_Commerce.Application.Common.Contracts.Payments.DTOs;

namespace E_Commerce.Application.Common.Contracts.Payments;


//  Stripe , PayPal  , PayMob , etc
public interface IPaymentGateway
{
    Task<CreatePaymentResponse> CreatePaymentAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default);

    Task<VerifyPaymentResponse> VerifyPaymentAsync(
        VerifyPaymentRequest request,
        CancellationToken cancellationToken = default);

    Task RefundPaymentAsync(
        RefundPaymentRequest request,
        CancellationToken cancellationToken = default);

    // Verify the HMAC/signature of an incoming webhook payload. Implementations
    // should perform constant-time comparison and return true only when the
    // signature matches the computed value for the payload.
    bool VerifyWebhookSignature(string rawPayload, string receivedHmac);
}