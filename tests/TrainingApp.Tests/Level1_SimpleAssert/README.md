# Level 1: かんたんな Assert

## これから何をするか

テストを Arrange(準備) → Act(実行) → Assert(検証) の3ステップ(3Aパターン)で書きます。

## なぜそうするか

テストの構造を毎回揃えることで、「何を準備し」「何を実行し」「何を確認しているか」が誰にでも一目でわかるようになります。これがすべてのテストの基本形です。

## 進め方

各テストには、3A(Arrange / Act / Assert)のコメントと、そこで何をするかを書いた `// 穴埋め:` コメントが置いてあります。1本目だけ Arrange のコードが入っていますが、2本目からは Arrange も自分で書きます。

1. `CalculatorServiceTests.cs` を開き、`[Fact(Skip = "要件: ...")]` の Skip を1つ外す
2. `// 穴埋め:` の指示にしたがって、そのコメントの下に中身を書く(書き方は下のサンプルコードを参照)
3. `dotnet test` で緑になったら、次の1本へ
4. プロダクションコード(`CalculatorService.cs`)は変更しない

## サンプルコード

```csharp
[Fact]
public void SayHello_名前を渡すと_挨拶文を返す()
{
    // Arrange
    // インスタンスを生成する
    var greeter = new Greeter();

    // Act
    // テストしたいメソッドを呼び出す
    var actual = greeter.SayHello("Alice");

    // Assert
    // メソッドの戻り値を検証
    Assert.Equal("Hello, Alice!", actual);
}
```
