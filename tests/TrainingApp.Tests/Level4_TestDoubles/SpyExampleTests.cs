using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level4_TestDoubles;

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
//
// 最初の1本は見本です。残りは Skip を外して自分たちで書いてください。
//
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
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

    // --- 見本(このテストは完成しています) ---
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
        // 評価しているのは spyNotifier 自身ではなく、このテストコード。ここがモックとの違い。
        Assert.Single(spyNotifier.NotifiedOrderIds);
        Assert.Contains(777, spyNotifier.NotifiedOrderIds);
    }

    // --- ここから先は自分たちで書く ---

    [Fact(Skip = "要件: 複数回注文すると、通知先にすべての注文IDが呼ばれた順に記録される")]
    public void 複数回注文するとき通知先にすべての注文IDが記録される()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }

    // --- 発展課題 ---
    // README.md の「発展課題」を参照。IPaymentGateway のスパイを手書きし、
    // 「どの金額で決済が呼ばれたか」を記録・検証するテストを追加してください。
    // (スパイのクラスもこのクラスの中に自分たちで定義します)
}
