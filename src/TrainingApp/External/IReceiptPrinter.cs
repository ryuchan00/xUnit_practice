namespace TrainingApp.External;

/// <summary>領収書を印刷(出力)する。PlaceOrder の処理自体には関与しない。</summary>
public interface IReceiptPrinter
{
    void Print(int orderId);
}
