# Level 3: Act した後の状態変化を確認する

## これから何をするか

戻り値ではなく、Act の実行によってオブジェクトの「状態」がどう変わったかを検証します。

## なぜそうするか

戻り値を持たないメソッド(`void` を返すメソッドなど)は、状態の変化そのものを確認しないとテストになりません。実務でもよく出会うパターンです。

## サンプルコード

```csharp
[Fact]
public void Increment_呼び出すと_Countが1増える()
{
    // Arrange
    var counter = new Counter();

    // Act
    counter.Increment();

    // Assert
    Assert.Equal(1, counter.Count);
}
```
