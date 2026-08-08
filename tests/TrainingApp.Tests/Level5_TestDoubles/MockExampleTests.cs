using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_TestDoubles;

// ===================================================================
// テストダブル 3: モック (Mock)
// -------------------------------------------------------------------
// モックとは「特定のメソッドが、特定の引数で、期待した回数だけ呼ばれたか」を
// 事前に期待(Setup)し、事後に検証(Verify)するためのオブジェクトです。
// スタブが「戻り値に注目する」のに対し、モックは「呼ばれ方(振る舞い)に注目する」
// 点が違いです。
//
// ここでは「決済が成功したら、必ず正しい注文IDで通知が呼ばれること」を
// モックで検証します。
// ===================================================================
public class MockExampleTests
{
    [Fact]
    public void PlaceOrder_決済成功時に_Notifierが正しい注文IDで1回だけ呼ばれる()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(true, "TXN-MOCK"));

        var mockNotifier = new Mock<IOrderNotifier>();
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, mockNotifier.Object, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 123, amount: 500m, cardToken: "tok_any");

        // Assert
        // 穴埋め: mockNotifier.Object.NotifyOrderCompleted(123) がちょうど1回呼ばれたことを検証してください
        // ヒント: mockNotifier.Verify(n => n.NotifyOrderCompleted(123), Times.Once);
        Assert.Fail("TODO: mockNotifier.Verify を使って検証してください");
    }

    [Fact]
    public void PlaceOrder_決済失敗時は_Notifierが一度も呼ばれない()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(false, TransactionId: string.Empty));

        var mockNotifier = new Mock<IOrderNotifier>();
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, mockNotifier.Object, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 123, amount: 500m, cardToken: "tok_any");

        // Assert
        // 穴埋め: mockNotifier.Object.NotifyOrderCompleted が一度も呼ばれていないことを検証してください
        // ヒント: Times.Never を使います
        Assert.Fail("TODO: mockNotifier.Verify(Times.Never) を使って検証してください");
    }

    [Fact]
    public void PlaceOrder_PaymentGatewayが正しい金額とトークンで呼ばれる()
    {
        // Arrange
        var mockPaymentGateway = new Mock<IPaymentGateway>();
        mockPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(true, "TXN-MOCK"));

        var notifier = new Mock<IOrderNotifier>().Object;
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(mockPaymentGateway.Object, notifier, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 1, amount: 1500m, cardToken: "tok_abc");

        // Assert
        // 穴埋め: mockPaymentGateway.Object.Charge(1500m, "tok_abc") がちょうど1回呼ばれたことを検証してください
        Assert.Fail("TODO: mockPaymentGateway.Verify を使って検証してください");
    }
}
