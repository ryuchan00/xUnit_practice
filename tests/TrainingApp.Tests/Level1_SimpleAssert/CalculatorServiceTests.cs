using TrainingApp.Services;

namespace TrainingApp.Tests.Level1_SimpleAssert;

// ===================================================================
// Level 1: かんたんな Assert
// -------------------------------------------------------------------
// 目的: 3A パターン(Arrange-Act-Assert)の型を身につける。
// 進め方: 各テストの "穴埋め" コメントの部分だけを書き換えてください。
//         プロダクションコード(CalculatorService.cs)は変更しないでください。
// ===================================================================
public class CalculatorServiceTests
{
    [Fact]
    public void Add_2と3を渡すと_5を返す()
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
    public void Subtract_5から3を引くと_2を返す()
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
    public void Divide_0で割ると_DivideByZeroExceptionを投げる()
    {
        // Arrange
        var calculator = new CalculatorService();

        // Act & Assert
        // 穴埋め: 例外が投げられることを検証する Assert を書いてください
        // ヒント: Assert.Throws<TException>(() => ...) を使います
        Assert.Fail("TODO: Assert.Throws を使って検証してください");
    }
}
