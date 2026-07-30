using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using E_Commerce.Application.Common.Contracts.Payments;
using E_Commerce.Application.Common.Contracts.Payments.DTOs;
using Microsoft.Extensions.Options;

namespace E_Commerce.Infrastructure.Services.Payment;

public sealed class PaymobGateway : IPaymentGateway
{
    private readonly HttpClient _http;
    private readonly PaymobOptions _options;

    public PaymobGateway(HttpClient http, IOptions<PaymobOptions> options)
    {
        _http = http;
        _options = options.Value;

        if (!string.IsNullOrWhiteSpace(_options.BaseUrl))
            _http.BaseAddress = new Uri(_options.BaseUrl);
    }

    public async Task<CreatePaymentResponse> CreatePaymentAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.Amount <= 0)
        {
            return new CreatePaymentResponse(
                Success: false,
                CheckoutUrl: null,
                TransactionId: null,
                PaymentIntentId: null,
                ErrorMessage: "Invalid payment amount.");
        }

        try
        {
            var payload = new
            {
                amount = (int)(request.Amount * 100),
                currency = request.Currency,
                billing_data = new
                {
                    email = string.IsNullOrWhiteSpace(request.CustomerEmail) ? "NA" : request.CustomerEmail,
                    first_name = string.IsNullOrWhiteSpace(request.CustomerName) ? "NA" : request.CustomerName,
                    phone_number = "NA"
                },
                special_reference = request.PaymentId.ToString(),
                notification_url = _options.WebhookUrl,
                redirection_url = _options.RedirectUrl
            };

            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "v1/intention/")
            {
                Content = JsonContent.Create(payload)
            };

            // Use SecretKey for server-to-server requests
            if (!string.IsNullOrWhiteSpace(_options.SecretKey))
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Token", _options.SecretKey);

            using var response = await _http.SendAsync(httpRequest, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                return new CreatePaymentResponse(
                    Success: false,
                    CheckoutUrl: null,
                    TransactionId: null,
                    PaymentIntentId: null,
                    ErrorMessage: $"Paymob returned {(int)response.StatusCode}: {errorBody}");
            }

            var result = await response.Content.ReadFromJsonAsync<PaymobIntentionResponse>(
                cancellationToken: cancellationToken);

            if (result is null || string.IsNullOrWhiteSpace(result.ClientSecret))
            {
                return new CreatePaymentResponse(
                    Success: false,
                    CheckoutUrl: null,
                    TransactionId: null,
                    PaymentIntentId: null,
                    ErrorMessage: "Paymob response did not contain a client secret.");
            }

            var checkoutUrl =
                $"{_options.CheckoutBaseUrl}?publishable_key={_options.PublicKey}&client_secret={result.ClientSecret}";

            return new CreatePaymentResponse(
                Success: true,
                CheckoutUrl: checkoutUrl,
                TransactionId: result.Id,
                PaymentIntentId: result.Id,
                ErrorMessage: null);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return new CreatePaymentResponse(
                Success: false,
                CheckoutUrl: null,
                TransactionId: null,
                PaymentIntentId: null,
                ErrorMessage: $"Could not reach Paymob: {ex.Message}");
        }
    }

    public async Task<VerifyPaymentResponse> VerifyPaymentAsync(
        VerifyPaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.TransactionId))
        {
            return new VerifyPaymentResponse(
                Success: false,
                IsPaid: false,
                TransactionId: null,
                PaymentIntentId: null,
                ErrorMessage: "Transaction Id is required.");
        }

        try
        {
            using var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"v1/transactions/{request.TransactionId}");

            if (!string.IsNullOrWhiteSpace(_options.SecretKey))
                httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Token", _options.SecretKey);

            using var response = await _http.SendAsync(httpRequest, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new VerifyPaymentResponse(
                    Success: false,
                    IsPaid: false,
                    TransactionId: request.TransactionId,
                    PaymentIntentId: null,
                    ErrorMessage: $"Paymob returned {(int)response.StatusCode}.");
            }

            var result = await response.Content.ReadFromJsonAsync<PaymobTransactionResponse>(
                cancellationToken: cancellationToken);

            return new VerifyPaymentResponse(
                Success: true,
                IsPaid: result?.Success ?? false,
                TransactionId: request.TransactionId,
                PaymentIntentId: result?.Order?.Id.ToString(),
                ErrorMessage: (result?.Success ?? false) ? null : "Payment not completed.");
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            return new VerifyPaymentResponse(
                Success: false,
                IsPaid: false,
                TransactionId: request.TransactionId,
                PaymentIntentId: null,
                ErrorMessage: $"Could not reach Paymob: {ex.Message}");
        }
    }

    public async Task RefundPaymentAsync(
        RefundPaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.TransactionId))
            throw new InvalidOperationException("Transaction Id is required.");

        var payload = new
        {
            transaction_id = request.TransactionId,
            amount_cents = (int)(request.Amount * 100)
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "v1/acceptance/void_refund/refund")
        {
            Content = JsonContent.Create(payload)
        };

        if (!string.IsNullOrWhiteSpace(_options.SecretKey))
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Token", _options.SecretKey);

        using var response = await _http.SendAsync(httpRequest, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException($"Paymob refund failed ({(int)response.StatusCode}): {errorBody}");
        }
    }

    public bool VerifyWebhookSignature(string rawPayload, string receivedHmac)
    {
        if (string.IsNullOrWhiteSpace(rawPayload) || string.IsNullOrWhiteSpace(receivedHmac))
            return false;

        // Default implementation: compute HMAC over raw payload using configured secret.
        // Some Paymob integrations require concatenation of fields; update helper when needed.
        var computed = ComputeHmacSha512(rawPayload, _options.HMACSecret);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(computed.ToLowerInvariant()),
            Encoding.UTF8.GetBytes(receivedHmac.ToLowerInvariant()));
    }

    private static string ComputeHmacSha512(string data, string secret)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secret ?? string.Empty));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToHexString(hash);
    }

    private sealed record PaymobIntentionResponse(string Id, string ClientSecret);
    private sealed record PaymobTransactionResponse(bool Success, PaymobOrder? Order);
    private sealed record PaymobOrder(long Id);
}

internal static class PaymobHmacHelper
{
    public static string BuildHmacConcatenation(string rawPayload)
    {
        // Default to raw payload; if your Paymob integration requires concatenating
        // specific fields in a defined order, implement that logic here per Paymob docs.
        return rawPayload;
    }
}

public sealed class FakePaymobGateway : IPaymentGateway
{
    public Task<CreatePaymentResponse> CreatePaymentAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.Amount <= 0)
        {
            return Task.FromResult(new CreatePaymentResponse(
                Success: false,
                CheckoutUrl: null,
                TransactionId: null,
                PaymentIntentId: null,
                ErrorMessage: "Invalid payment amount."));
        }

        var paymentId = Guid.NewGuid().ToString("N");

        return Task.FromResult(new CreatePaymentResponse(
            Success: true,
            CheckoutUrl: $"https://fake-paymob.local/checkout/{paymentId}",
            TransactionId: $"txn_{paymentId}",
            PaymentIntentId: $"pay_{paymentId}",
            ErrorMessage: null));
    }

    public Task<VerifyPaymentResponse> VerifyPaymentAsync(
        VerifyPaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.TransactionId))
        {
            return Task.FromResult(new VerifyPaymentResponse(
                Success: false,
                IsPaid: false,
                TransactionId: null,
                PaymentIntentId: null,
                ErrorMessage: "Transaction Id is required."));
        }

        // Simulate failure if the transaction id contains "failed".
        var isPaid = !request.TransactionId.Contains("failed", StringComparison.OrdinalIgnoreCase);

        return Task.FromResult(new VerifyPaymentResponse(
            Success: true,
            IsPaid: isPaid,
            TransactionId: request.TransactionId,
            PaymentIntentId: $"pay_{request.TransactionId}",
            ErrorMessage: isPaid ? null : "Payment failed."));
    }

    public Task RefundPaymentAsync(RefundPaymentRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.TransactionId))
            throw new InvalidOperationException("Transaction Id is required.");

        // Simulate a successful refund.
        return Task.CompletedTask;
    }

    /// <summary>Always valid in Development — nothing is actually signed.</summary>
    public bool VerifyWebhookSignature(string rawPayload, string receivedHmac) => true;
}
