namespace TrainingApp.External;

public record PaymentResult(bool IsSuccess, string TransactionId);

/// <summary>
/// 外部の決済APIを呼び出すゲートウェイ。
/// 実装(PaymentGateway)はネットワーク越しに外部サービスを呼び出すため、
/// ユニットテストでは直接使わず、テストダブルに差し替えて使う。
/// </summary>
public interface IPaymentGateway
{
    PaymentResult Charge(decimal amount, string cardToken);
}
