using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_TestDoubles;

// ===================================================================
// テストダブル 5: フェイク (Fake)
// -------------------------------------------------------------------
// フェイクとは「本物の代わりに使える、簡易的だが実際に動くロジックを
// 持ったオブジェクト」です。スタブは「決まった値を返すだけ」でしたが、
// フェイクは入力に応じて自分で判断する、ミニチュア版の本物です。
//
// ここでは「1万円を超える決済は失敗する」という単純なビジネスルールを
// 実装した FakePaymentGateway を使います。外部APIを呼ばずに、
// 本物に近い振る舞いをテストできます。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_TestDoubles"
// ===================================================================
public class FakeExampleTests
{
    /// <summary>
    /// 手書きのフェイク実装。
    /// 「1万円を超える決済は失敗する」という簡易ロジックを実際に持っている点が、
    /// 常に同じ値を返すだけのスタブとの違い。
    /// </summary>
    private class FakePaymentGateway : IPaymentGateway
    {
        private const decimal CreditLimit = 10_000m;

        public PaymentResult Charge(decimal amount, string cardToken)
        {
            if (amount > CreditLimit)
            {
                return new PaymentResult(false, TransactionId: string.Empty);
            }

            return new PaymentResult(true, $"TXN-FAKE-{amount}");
        }
    }

    [Fact]
    public void 上限内の金額のとき決済は成功する()
    {
        // Arrange
        var fakePaymentGateway = new FakePaymentGateway();
        var notifier = new Mock<IOrderNotifier>().Object;
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(fakePaymentGateway, notifier, receiptPrinter);

        // Act
        var actual = orderService.PlaceOrder(orderId: 1, amount: 9_999m, cardToken: "tok_any");

        // Assert
        // 穴埋め: actual.IsSuccess が true であることを検証してください
        Assert.Fail("TODO: actual.IsSuccess を検証してください");
    }

    [Fact]
    public void 上限を超える金額のとき決済は失敗する()
    {
        // Arrange
        var fakePaymentGateway = new FakePaymentGateway();
        var notifier = new Mock<IOrderNotifier>().Object;
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(fakePaymentGateway, notifier, receiptPrinter);

        // Act
        var actual = orderService.PlaceOrder(orderId: 1, amount: 10_001m, cardToken: "tok_any");

        // Assert
        // 穴埋め: actual.IsSuccess が false であることを検証してください
        Assert.Fail("TODO: actual.IsSuccess を検証してください");
    }
}
