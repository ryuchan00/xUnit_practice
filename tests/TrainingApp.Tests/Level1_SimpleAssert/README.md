# Level 1: かんたんな Assert

## これから何をするか

テストを **Arrange(準備) → Act(実行) → Assert(検証)** の3ステップ(3Aパターン)で書きます。

## なぜそうするか

テストの構造を毎回揃えることで、「何を準備し」「何を実行し」「何を確認しているか」が誰にでも一目でわかるようになります。これがすべてのテストの基本形です。

## サンプルコード

```csharp
[Fact]
public void SayHello_名前を渡すと_挨拶文を返す()
{
    // Arrange
    var greeter = new Greeter();

    // Act
    var actual = greeter.SayHello("Alice");

    // Assert
    Assert.Equal("Hello, Alice!", actual);
}
```
