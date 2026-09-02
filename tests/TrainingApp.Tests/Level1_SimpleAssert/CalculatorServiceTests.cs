using TrainingApp.Services;

namespace TrainingApp.Tests.Level1_SimpleAssert;

// ===================================================================
// Level 1: かんたんな Assert
// -------------------------------------------------------------------
// 目的: 3A パターン(Arrange-Act-Assert)の型を身につける。
// 進め方: 用意してあるのは Arrange(準備)と、3A のコメントだけです。
//         [Fact(Skip = "...")] の Skip を外し、// Act と // Assert の下に
//         中身を自分で書いてください。書き方は README.md のサンプルを参照。
//         プロダクションコード(CalculatorService.cs)は変更しないでください。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level1_SimpleAssert"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level1_SimpleAssert"
// ===================================================================
public class CalculatorServiceTests
{
    [Fact(Skip = "要件: 2 と 3 を足すと 5 が返る")]
    public void 二つの数値を足すとき合計が返る()
    {
        // Arrange
        var calculator = new CalculatorService();

        // Act

        // Assert
    }

    [Fact(Skip = "要件: 5 から 3 を引くと 2 が返る")]
    public void 大きい数から小さい数を引くとき差が返る()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(Skip = "要件: 0 で割ろうとすると例外が発生する。ヒント: Assert.Throws<TException>(() => ...)")]
    public void ゼロで割ろうとするとき例外が発生する()
    {
        // Arrange
        // 穴埋め: CalculatorService のインスタンスを用意してください

        // Act & Assert
        // 例外を検証するときは Act と Assert が1行にまとまるので、
        // このテストだけ「Act & Assert」というコメントにしてある。
        // 穴埋め: 10 を 0 で割る計算(Divide)を呼び出すと例外が投げられることを検証してください
        //         (Assert.Throws<TException>(() => ...) を使う)
    }
}
