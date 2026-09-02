using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level4_TestDoubles;

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
//
// 最初の1本は見本です。残りは Skip を外して自分たちで書いてください。
//
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
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

    // --- 見本(このテストは完成しています) ---
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
        Assert.True(actual.IsSuccess);
    }

    // --- ここから先は自分たちで書く ---

    [Fact(Skip = "要件: 上限を超える金額(10,001円)のとき、決済は失敗する")]
    public void 上限を超える金額のとき決済は失敗する()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }

    // 境界値のテスト。書く前に「上限ちょうど(10,000円)は成功すべきか、失敗すべきか」を
    // 自分で決めてから、FakePaymentGateway の実装を読んで答え合わせをしてください。
    // メソッド名は途中までです。決めた振る舞いが分かる名前に書き換えてください。
    [Fact(Skip = "要件: 上限ちょうどの金額(10,000円)のとき、決済はどうなるべきか")]
    public void 上限ちょうどの金額のとき()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }
}
