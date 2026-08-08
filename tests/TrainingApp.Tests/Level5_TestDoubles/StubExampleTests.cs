using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_TestDoubles;

// ===================================================================
// テストダブル 2: スタブ (Stub)
// -------------------------------------------------------------------
// スタブとは「あらかじめ決められた戻り値(缶詰の回答)を返すだけのオブジェクト」
// です。呼ばれたかどうかや、どんな引数で呼ばれたかは検証しません。
// あくまで「テスト対象に決まった入力を与えるための道具」として使います。
//
// ここでは IPaymentGateway を「決済は必ず成功する」というスタブに差し替え、
// OrderService がその結果を使ってどう振る舞うかをテストします。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// ===================================================================
public class StubExampleTests
{
    [Fact]
    public void 決済が成功するスタブを渡すとき注文も成功する()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        // 穴埋め: どんな引数で Charge が呼ばれても、成功する PaymentResult を返すよう Setup してください
        // ヒント: stubPaymentGateway.Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
        //                           .Returns(new PaymentResult(true, "TXN-STUB"));

        var notifier = new Mock<IOrderNotifier>().Object;
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, notifier, receiptPrinter);

        // Act
        var actual = orderService.PlaceOrder(orderId: 1, amount: 500m, cardToken: "tok_any");

        // Assert
        // 穴埋め: actual.IsSuccess が true であることを検証してください
        Assert.Fail("TODO: actual.IsSuccess を検証してください");
    }

    [Fact]
    public void 決済が失敗するスタブを渡すとき注文も失敗する()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(false, TransactionId: string.Empty));

        var notifier = new Mock<IOrderNotifier>().Object;
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, notifier, receiptPrinter);

        // Act
        var actual = orderService.PlaceOrder(orderId: 1, amount: 500m, cardToken: "tok_any");

        // Assert
        // 穴埋め: actual.IsSuccess が false であることを検証してください
        Assert.Fail("TODO: actual.IsSuccess を検証してください");
    }
}
