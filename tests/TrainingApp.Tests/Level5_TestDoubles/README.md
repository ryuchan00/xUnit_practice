# Level 5: テストダブル

## これから何をするか

外部依存を「ダミー」「スタブ」「モック」「スパイ」「フェイク」という5種類のテストダブルに差し替える方法を、それぞれ学びます。

## なぜそうするか

外部API通信のような不安定・低速な依存をそのままテストすると、テストが遅くなったり、ネットワーク状態によって結果が変わったりします。依存をテストダブルに差し替えることで、自分のコードのロジックだけを高速かつ安定して検証できます。5種類の違いは「呼ばれ方を検証するか」「値を記録するだけか」など、目的によって使い分けます。

## サンプルコード(スタブの例)

```csharp
[Fact]
public void GetPrice_為替レートAPIが1ドル150円を返すとき_日本円換算額を返す()
{
    // Arrange
    var stubExchangeRateApi = new Mock<IExchangeRateApi>();
    stubExchangeRateApi
        .Setup(api => api.GetUsdToJpyRate())
        .Returns(150m);
    var priceService = new PriceService(stubExchangeRateApi.Object);

    // Act
    var actual = priceService.ConvertToJpy(10m);

    // Assert
    Assert.Equal(1500m, actual);
}
```
