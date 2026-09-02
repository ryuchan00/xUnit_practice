# Level 5: Controller のテスト(Controller Spec)

## これから何をするか

ASP.NET Core の Controller を直接 `new` し、依存(リポジトリなど)を Moq でモック化したうえで、`ActionResult` の型(`OkObjectResult` / `NotFoundResult` など)を検証します。

## なぜそうするか

Controller は「HTTPリクエストを受けて、適切なレスポンス(ステータスコード)を返す」責務を持ちます。この責務が正しく果たされているかを、実際のHTTP通信なしに高速に確認できます。

## サンプルコード

```csharp
[Fact]
public void GetById_存在しないIdを渡すと_NotFoundを返す()
{
    // Arrange
    var mockRepository = new Mock<IBookRepository>();
    mockRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns((Book?)null);
    var controller = new BooksController(mockRepository.Object);

    // Act
    var actual = controller.GetById(999);

    // Assert
    Assert.IsType<NotFoundResult>(actual.Result);
}
```
