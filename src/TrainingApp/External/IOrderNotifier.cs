namespace TrainingApp.External;

/// <summary>注文完了を(メールなどで)通知する。外部サービス呼び出しを伴う。</summary>
public interface IOrderNotifier
{
    void NotifyOrderCompleted(int orderId);
}
