using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_TestDoubles;

// ===================================================================
// テストダブル 4: スパイ (Spy)
// -------------------------------------------------------------------
// 参考: http://xunitpatterns.com/Mock%20Object.html (Gerard Meszaros)
//
// モックとスパイの本質的な違いは「間接出力の評価をどこで行うか」です。
//   モック = テストダブル自身が評価する(Self Verifying。例: mock.Verify(...))
//   スパイ = テストダブルは記録するだけで、評価はテストコード側が行う
//
// ここでは Moq を使わず、手書きの SpyOrderNotifier クラスでこの仕組みを
// 体感します。あらかじめ「呼ばれるはずだ」という期待は一切持たず、
// ただ呼び出された内容を記録するだけ。検証(Assert)はテストコード側で行います。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
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
    public void 決済が成功するとき通知先に注文IDが記録される()
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
    public void 複数回注文するとき通知先にすべての注文IDが記録される()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        // 穴埋め: どんな引数で Charge が呼ばれても、成功する PaymentResult を返すよう Setup してください
        // ヒント: new PaymentResult(true, "TXN-SPY") を返すようにする

        var spyNotifier = new SpyOrderNotifier();
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, spyNotifier, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 1, amount: 100m, cardToken: "tok_1");
        orderService.PlaceOrder(orderId: 2, amount: 200m, cardToken: "tok_2");

        // Assert
        // このテストは Arrange を正しく穴埋めできれば緑になります。
        // (穴埋めが無い間は Charge が null を返すため NullReferenceException で落ちます)
        Assert.Equal(new List<int> { 1, 2 }, spyNotifier.NotifiedOrderIds);
    }
}
