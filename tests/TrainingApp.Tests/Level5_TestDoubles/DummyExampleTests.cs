using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_TestDoubles;

// ===================================================================
// テストダブル 1: ダミー (Dummy)
// -------------------------------------------------------------------
// ダミーとは「引数として渡す必要はあるが、テスト対象のロジックの中では
// 一切使われないオブジェクト」です。コンパイルを通すため・コンストラクタの
// 要求を満たすためだけに存在します。
//
// OrderService.PlaceOrder() は IReceiptPrinter を使いません
// (PrintReceipt() でのみ使われます)。そのため PlaceOrder のテストでは
// IReceiptPrinter は「ダミー」として渡すだけで十分です。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// ===================================================================
public class DummyExampleTests
{
    [Fact]
    public void 注文処理では領収書印刷は使われないためダミーで十分足りる()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(true, "TXN-001"));
        var dummyNotifier = new Mock<IOrderNotifier>();

        // ここでは IReceiptPrinter の「ダミー」として null! を渡しています。
        // PlaceOrder の中で IReceiptPrinter は一切使われないため、これで問題なく動作します。
        // (もし誤って Print が呼ばれれば NullReferenceException になり、
        //  「本来呼ばれないはずの依存が呼ばれた」というバグにすぐ気づけます)
        IReceiptPrinter dummyReceiptPrinter = null!;

        var orderService = new OrderService(stubPaymentGateway.Object, dummyNotifier.Object, dummyReceiptPrinter);

        // Act
        var actual = orderService.PlaceOrder(orderId: 1, amount: 1000m, cardToken: "tok_test");

        // Assert
        // 穴埋め: actual.IsSuccess が true であることを検証してください
        // (dummyReceiptPrinter.Print が呼ばれたかどうかは、このテストの関心事ではありません)
        Assert.Fail("TODO: actual.IsSuccess を検証してください");
    }
}
