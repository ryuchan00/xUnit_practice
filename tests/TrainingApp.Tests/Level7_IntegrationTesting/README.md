# Level 7: DIコンテナと WebApplicationFactory を使った結合テスト

## これから何をするか

`WebApplicationFactory<Program>` でアプリを実際に起動し、DIコンテナに登録されたサービスをモックに差し替えたうえで、実際の HTTP エンドポイントにリクエストを送って検証します。

## なぜそうするか

Level4 の Controller テストは Controller クラス単体の振る舞いしか確認できません。ここではルーティング・モデルバインディング・JSONシリアライズまで含めて、アプリが実際に正しく動くことを確認します。

## サンプルコード

```csharp
public class BooksApiFactory : WebApplicationFactory<Program>
{
    public Mock<IBookRepository> RepositoryMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IBookRepository>();
            services.AddSingleton(RepositoryMock.Object);
        });
    }
}

public class BooksEndpointTests : IClassFixture<BooksApiFactory>
{
    private readonly BooksApiFactory _factory;

    public BooksEndpointTests(BooksApiFactory factory) => _factory = factory;

    [Fact]
    public async Task GetBooks_呼び出すと_200OKが返る()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/books");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
```
