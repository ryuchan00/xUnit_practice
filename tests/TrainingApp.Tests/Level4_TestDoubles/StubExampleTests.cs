using Moq;
using TrainingApp.External;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level4_TestDoubles;

// ===================================================================
// テストダブル 2: スタブ (Stub)
// -------------------------------------------------------------------
// スタブとは「あらかじめ決められた戻り値(缶詰の回答)を返すだけのオブジェクト」
// です。呼ばれたかどうかや、どんな引数で呼ばれたかは検証しません。
// あくまで「テスト対象に決まった入力を与えるための道具」として使います。
//
// ここでは IPaymentGateway を「決済は必ず成功する」というスタブに差し替え、
// OrderService がその結果を使ってどう振る舞うかをテストします。
//
// 【スタブが要る理由は2つ】
// 1. 外部APIを実際に叩かないため。本物の決済ゲートウェイを使えば、テストを
//    実行するたびに本当に課金されてしまう。スタブに差し替えて、呼び出し側
//    (OrderService)が通常どおり動くことだけを確かめる。
// 2. 意図的に起こすのが難しい異常系を再現するため。「通信エラー」「タイムアウト」
//    のような状況は、本物相手に毎回同じようには起こせない。3本目の課題がこれ。
//
// 最初の1本は見本です。残りは Skip を外して自分たちで書いてください。
//
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level4_TestDoubles"
// ===================================================================
public class StubExampleTests
{
    // --- 見本(このテストは完成しています) ---
    [Fact]
    public void 決済が成功するスタブを渡すとき注文も成功する()
    {
        // Arrange
        var stubPaymentGateway = new Mock<IPaymentGateway>();
        stubPaymentGateway
            .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
            .Returns(new PaymentResult(true, "TXN-STUB"));

        var notifier = new Mock<IOrderNotifier>().Object;
        var receiptPrinter = new Mock<IReceiptPrinter>().Object;
        var orderService = new OrderService(stubPaymentGateway.Object, notifier, receiptPrinter);

        // Act
        var actual = orderService.PlaceOrder(orderId: 1, amount: 500m, cardToken: "tok_any");

        // Assert
        Assert.True(actual.IsSuccess);
    }

    // --- ここから先は自分たちで書く ---

    [Fact(Skip = "要件: 決済が失敗するスタブを渡すと、注文も失敗する。失敗は new PaymentResult(false, string.Empty) で表す")]
    public void 決済が失敗するスタブを渡すとき注文も失敗する()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }

    // ここからがこの Level の山場です。
    //
    // 「決済APIが通信エラーで例外を投げる」という状況は、本物の API を相手にしていては
    // ほぼ再現できません。ネットワークを切る、相手のサーバを落とす、といった芸当が必要になり、
    // しかも毎回同じようには起こせません。だからテストを書くのを諦める
    //   → 異常系だけテストが無い
    //   → 本番で初めて「例外が握りつぶされていた」と気づく
    // というのが、よくある事故の形です。
    //
    // スタブは、この「起こせない状況」を確実に起こすための道具です。
    // Moq では .Returns(...) の代わりに .Throws(...) を使うと、例外を投げるスタブになります。
    //   stubPaymentGateway
    //       .Setup(g => g.Charge(It.IsAny<decimal>(), It.IsAny<string>()))
    //       .Throws(new HttpRequestException("接続できませんでした"));
    //
    // 書く前に「このとき OrderService はどう振る舞うべきか」を先に決めてください。
    // 例外を握りつぶして失敗として返すべきか、そのまま呼び出し元に伝えるべきか。
    // 決めてから OrderService の実装を読み、答え合わせをしましょう。
    //
    // メソッド名はあえて途中までにしてあります。振る舞いを決めたら、
    // 「〜するとき〜になる」と分かる名前に自分で書き換えてください。
    [Fact(Skip = "要件: 決済APIが通信エラー(HttpRequestException)を投げるとき、OrderService はどう振る舞うべきか")]
    public void 決済APIが通信エラーを投げるとき()
    {
        // Arrange

        // Act
        // 例外がそのまま呼び出し元に伝わる想定でテストするなら、
        // Assert.Throws<T>(() => ...) で Act と Assert が1つにまとまる。
        // その場合は下の2つを「// Act & Assert」に書き換えてよい。

        // Assert
        Assert.Fail("TODO: ここを検証に書き換える");
    }
}
