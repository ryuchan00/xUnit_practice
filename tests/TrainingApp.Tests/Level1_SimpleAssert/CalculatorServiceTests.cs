using TrainingApp.Services;

namespace TrainingApp.Tests.Level1_SimpleAssert;

// ===================================================================
// Level 1: かんたんな Assert
// -------------------------------------------------------------------
// 目的: 3A パターン(Arrange-Act-Assert)の型を身につける。
// 進め方: 各テストの "穴埋め" コメントの部分だけを書き換えてください。
//         プロダクションコード(CalculatorService.cs)は変更しないでください。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level1_SimpleAssert"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level1_SimpleAssert"
// ===================================================================
public class CalculatorServiceTests
{
    [Fact]
    public void 二つの数値を足すとき合計が返る()
    {
        // Arrange
        var calculator = new CalculatorService();

        // Act
        var actual = calculator.Add(2, 3);

        // Assert
        // 穴埋め: actual が期待した値になっていることを検証してください
        Assert.Equal(0, actual);
    }

    [Fact]
    public void 大きい数から小さい数を引くとき差が返る()
    {
        // Arrange
        var calculator = new CalculatorService();

        // Act
        var actual = calculator.Subtract(5, 3);

        // Assert
        // 穴埋め: 正しい期待値に書き換えてください
        Assert.Equal(0, actual);
    }

    [Fact]
    public void ゼロで割ろうとするとき例外が発生する()
    {
        // Arrange
        var calculator = new CalculatorService();

        // Act & Assert
        // 穴埋め: 例外が投げられることを検証する Assert を書いてください
        // ヒント: Assert.Throws<TException>(() => ...) を使います
        Assert.Fail("TODO: Assert.Throws を使って検証してください");
    }
}
