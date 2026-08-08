namespace TrainingApp.External;

public class ReceiptPrinter : IReceiptPrinter
{
    public void Print(int orderId)
    {
        Console.WriteLine($"注文 {orderId} の領収書を印刷しました。");
    }
}
