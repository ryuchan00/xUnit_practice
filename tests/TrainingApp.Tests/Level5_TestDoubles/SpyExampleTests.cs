using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_TestDoubles;

// ===================================================================
// テストダブル 4: スパイ (Spy)
// -------------------------------------------------------------------
// スパイとは「実際に呼ばれた内容(引数・回数)を自分で記録しておき、
// テストの後半でその記録を検証する」オブジェクトです。
//
// モックとの違い:
//   モック   = 「呼ばれるはずだ」という期待を"事前に"設定し、フレームワークが自動検証する
//   スパイ   = 呼び出しの記録だけを取っておき、"事後に"自分でその記録を検証する
//
// ここでは Moq を使わず、手書きの SpyOrderNotifier クラスで
// スパイの仕組みそのものを体感します。
// ===================================================================
public class SpyExampleTests
{
    /// <summary>
    /// 手書きのスパイ。呼び出された注文IDをすべて記録しておくだけで、
    /// あらかじめ「呼ばれるはずだ」という期待は持たない。
    /// </summary>
    private class SpyOrderNotifier : IOrderNotifier
    {
        public List<int> NotifiedOrderIds { get; } = [];

        public void NotifyOrderCompleted(int orderId)
        {
            NotifiedOrderIds.Add(orderId);
        }
    }

    [Fact]
    public void PlaceOrder_決済成功時に_スパイに通知内容が記録される()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(true, "TXN-SPY"));

        var spyNotifier = new SpyOrderNotifier();
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, spyNotifier, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 777, amount: 500m, cardToken: "tok_any");

        // Assert
        // 穴埋め1: spyNotifier.NotifiedOrderIds の件数が1件であることを検証してください
        // 穴埋め2: spyNotifier.NotifiedOrderIds に 777 が含まれることを検証してください (Assert.Contains)
        Assert.Fail("TODO: NotifiedOrderIds を検証してください");
    }

    [Fact]
    public void PlaceOrder_複数回呼び出すと_スパイにすべて記録される()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(true, "TXN-SPY"));

        var spyNotifier = new SpyOrderNotifier();
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, spyNotifier, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 1, amount: 100m, cardToken: "tok_1");
        orderService.PlaceOrder(orderId: 2, amount: 200m, cardToken: "tok_2");

        // Assert
        // 穴埋め: spyNotifier.NotifiedOrderIds が [1, 2] という並びで記録されていることを検証してください
        // ヒント: Assert.Equal(new List<int> { 1, 2 }, spyNotifier.NotifiedOrderIds);
        Assert.Fail("TODO: NotifiedOrderIds を検証してください");
    }
}
