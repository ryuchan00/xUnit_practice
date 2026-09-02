using System.Net;
using System.Net.Http.Json;
using Moq;
using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level7_IntegrationTesting;

// ===================================================================
// Level 7: DIコンテナと WebApplicationFactory を使った結合テスト
// -------------------------------------------------------------------
// Level5 では Controller クラスを直接 `new` してテストしました(コンストラクタ注入)。
// ここでは一歩進んで、アプリを実際に起動し、実際の HTTP エンドポイントに
// リクエストを送って検証します。ルーティング・モデルバインディング・
// JSONシリアライズまで含めて動作することを確認できる、より「本物」に近いテストです。
//
// ProductsApiFactory (WebApplicationFactory<Program>) が、Program.cs に登録済みの
// 本物の IProductRepository を取り除き、モックに差し替えています。
// これにより「実際のDIコンテナ経由でモックが注入される」ことを体感できます。
//
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level7_IntegrationTesting"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level7_IntegrationTesting"
// ===================================================================
public class ProductsEndpointTests : IClassFixture<ProductsApiFactory>
{
    private readonly ProductsApiFactory _factory;

    public ProductsEndpointTests(ProductsApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task 存在するIdにGETするとき商品が200OKで返る()
    {
        // Arrange
        var expected = new Product(1, "テスト商品", 1000m);
        _factory.RepositoryMock.Setup(r => r.GetById(1)).Returns(expected);
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/products/1");

        // Assert
        // 穴埋め1: response.StatusCode が HttpStatusCode.OK であることを検証してください
        // 穴埋め2: response.Content から Product を読み取り(ReadFromJsonAsync<Product>)、
        //          expected と一致することを検証してください
        Assert.Fail("TODO: レスポンスのステータスコードとボディを検証してください");
    }

    [Fact]
    public async Task 存在しないIdにGETするとき404NotFoundが返る()
    {
        // Arrange
        _factory.RepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns((Product?)null);
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/products/999");

        // Assert
        // 穴埋め: response.StatusCode が HttpStatusCode.NotFound であることを検証してください
        Assert.Fail("TODO: レスポンスのステータスコードを検証してください");
    }

    [Fact]
    public async Task 商品をPOSTするとき201CreatedとLocationヘッダーが返る()
    {
        // Arrange
        var input = new Product(0, "新商品", 500m);
        var saved = input with { Id = 42 };
        _factory.RepositoryMock.Setup(r => r.Add(input)).Returns(saved);
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/api/products", input);

        // Assert
        // 穴埋め1: response.StatusCode が HttpStatusCode.Created であることを検証してください
        // 穴埋め2: response.Headers.Location が null でないことを検証してください
        Assert.Fail("TODO: レスポンスのステータスコードと Location ヘッダーを検証してください");
    }
}
