using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level4_TestDoubles;

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
//
// 最初の1本は見本です。残りは Skip を外して自分たちで書いてください。
//
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
// ===================================================================
public class MockExampleTests
{
    // --- 見本(このテストは完成しています) ---
    [Fact]
    public void 決済が成功するとき正しい注文IDで通知が1回だけ行われる()
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
        // 「戻り値」ではなく「呼ばれ方」を検証している点が、スタブとの違い。
        mockNotifier.Verify(n => n.NotifyOrderCompleted(123), Times.Once);
    }

    // --- ここから先は自分たちで書く ---

    [Fact(Skip = "要件: 決済が失敗したときは、通知が一度も呼ばれない。ヒント: Times.Never")]
    public void 決済が失敗するとき通知は一度も行われない()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }

    [Fact(Skip = "要件: 注文すると、正しい金額とカードトークンで決済が呼び出される")]
    public void 注文するとき正しい金額とトークンで決済が呼び出される()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }
}
