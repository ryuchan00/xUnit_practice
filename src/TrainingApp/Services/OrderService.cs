using TrainingApp.External;
using TrainingApp.Models;

namespace TrainingApp.Services;

public class OrderService
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly IOrderNotifier _notifier;
    private readonly IReceiptPrinter _receiptPrinter;

    public OrderService(IPaymentGateway paymentGateway, IOrderNotifier notifier, IReceiptPrinter receiptPrinter)
    {
        _paymentGateway = paymentGateway;
        _notifier = notifier;
        _receiptPrinter = receiptPrinter;
    }

    /// <summary>
    /// 決済を実行し、成功した場合のみ注文完了を通知する。
    /// このメソッドは _receiptPrinter を一切使用しない点に注目。
    /// </summary>
    public OrderResult PlaceOrder(int orderId, decimal amount, string cardToken)
    {
        var result = _paymentGateway.Charge(amount, cardToken);

        if (result.IsSuccess)
        {
            _notifier.NotifyOrderCompleted(orderId);
        }

        return new OrderResult(orderId, result.IsSuccess);
    }

    public void PrintReceipt(int orderId)
    {
        _receiptPrinter.Print(orderId);
    }
}
