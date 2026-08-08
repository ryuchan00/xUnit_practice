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
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// ===================================================================
public class MockExampleTests
{
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
        // 穴埋め: mockNotifier.Object.NotifyOrderCompleted(123) がちょうど1回呼ばれたことを検証してください
        // ヒント: mockNotifier.Verify(n => n.NotifyOrderCompleted(123), Times.Once);
        Assert.Fail("TODO: mockNotifier.Verify を使って検証してください");
    }

    [Fact]
    public void 決済が失敗するとき通知は一度も行われない()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        // 穴埋め: どんな引数で Charge が呼ばれても、失敗する PaymentResult を返すよう Setup してください
        // ヒント: new PaymentResult(false, TransactionId: string.Empty) を返すようにする

        var mockNotifier = new Mock<IOrderNotifier>();
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, mockNotifier.Object, receiptPrinter);

        // Act
        orderService.PlaceOrder(orderId: 123, amount: 500m, cardToken: "tok_any");

        // Assert
        // このテストは Arrange を正しく穴埋めできれば緑になります。
        // (穴埋めが無い間は Charge が null を返すため NullReferenceException で落ちます)
        mockNotifier.Verify(n => n.NotifyOrderCompleted(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void 注文するとき正しい金額とトークンで決済が呼び出される()
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
