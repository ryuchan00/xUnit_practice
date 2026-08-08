namespace TrainingApp.External;

/// <summary>
/// 実際にメール送信APIを呼び出す本番実装。外部サービスに依存するため、
/// ユニットテストの対象には含めない。
/// </summary>
public class EmailOrderNotifier : IOrderNotifier
{
    public void NotifyOrderCompleted(int orderId)
    {
        // 実際にはメール送信APIなどを呼び出す想定。
        Console.WriteLine($"注文 {orderId} の完了メールを送信しました。");
    }
}
