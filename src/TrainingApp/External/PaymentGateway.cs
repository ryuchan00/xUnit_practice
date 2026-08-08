using System.Net.Http.Json;

namespace TrainingApp.External;

/// <summary>
/// 実際に外部決済サービスへHTTPリクエストを送る本番実装。
/// ネットワークに依存するため、ユニットテストの対象には含めない。
/// (結合テスト・E2Eテストの対象にする)
/// </summary>
public class PaymentGateway : IPaymentGateway
{
    private readonly HttpClient _httpClient;

    public PaymentGateway(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public PaymentResult Charge(decimal amount, string cardToken)
    {
        var response = _httpClient
            .PostAsJsonAsync("https://payment.example.com/api/charge", new { amount, cardToken })
            .GetAwaiter()
            .GetResult();

        response.EnsureSuccessStatusCode();

        return response.Content.ReadFromJsonAsync<PaymentResult>().GetAwaiter().GetResult()
               ?? throw new InvalidOperationException("決済APIから予期しない応答が返されました。");
    }
}
