using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level8_IntegrationTesting;

/// <summary>
/// アプリ全体(DIコンテナ・ルーティング・モデルバインディング・JSONシリアライズ)を
/// 実際に起動しつつ、外部依存である IProductRepository だけをモックに差し替えるための
/// WebApplicationFactory(= xUnit の Fixture)。
///
/// Level4/5 では Controller を直接 `new` してテストしていたが(コンストラクタ注入)、
/// ここでは Program.cs に実際に登録されている依存関係を、DIコンテナのレベルで
/// 差し替える点が異なる。
///
/// 穴埋め: ConfigureWebHost の中身を実装してください。
///   1. services.RemoveAll&lt;IProductRepository&gt;() で、Program.cs に登録済みの
///      本物の実装(InMemoryProductRepository)を取り除く
///      (RemoveAll は Microsoft.Extensions.DependencyInjection.Extensions 名前空間)
///   2. services.AddSingleton(RepositoryMock.Object) で、代わりに
///      このクラスが持つモックを登録する
/// </summary>
public class ProductsApiFactory : WebApplicationFactory<Program>
{
    public Mock<IProductRepository> RepositoryMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 穴埋め: 上記コメントの1, 2を実装してください
            throw new NotImplementedException("TODO: IProductRepository をモックに差し替えてください");
        });
    }
}
