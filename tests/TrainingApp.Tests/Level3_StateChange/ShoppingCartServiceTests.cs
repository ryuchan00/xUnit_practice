using TrainingApp.Services;

namespace TrainingApp.Tests.Level3_StateChange;

// ===================================================================
// Level 3: Act した後に状態が変化することを確認する
// -------------------------------------------------------------------
// これまでは戻り値を検証してきましたが、ここでは Act の実行によって
// オブジェクトの「状態」がどう変わるかを検証します。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level3_StateChange"
// Docker で実行する場合:
//   docker compose run --rm test --filter "FullyQualifiedName~TrainingApp.Tests.Level3_StateChange"
// ===================================================================
public class ShoppingCartServiceTests
{
    [Fact]
    public void 商品を1つ追加すると合計金額が加算される()
    {
        // Arrange
        var cart = new ShoppingCartService();

        // Act
        cart.AddItem("りんご", 100m, 3);

        // Assert
        // 穴埋め: cart.TotalAmount が 300 になっていることを検証してください
        Assert.Fail("TODO: cart.TotalAmount を検証してください");
    }

    [Fact]
    public void 商品を2種類追加すると合計個数が集計される()
    {
        // Arrange
        var cart = new ShoppingCartService();

        // Act
        cart.AddItem("りんご", 100m, 3);
        cart.AddItem("みかん", 50m, 2);

        // Assert
        // 穴埋め: cart.TotalItemCount が 5 になっていることを検証してください
        Assert.Fail("TODO: cart.TotalItemCount を検証してください");
    }

    [Fact]
    public void 追加した商品を削除すると一覧から消える()
    {
        // Arrange
        var cart = new ShoppingCartService();
        cart.AddItem("りんご", 100m, 3);

        // Act
        cart.RemoveItem("りんご");

        // Assert
        // 穴埋め: cart.Items が空になっていることを検証してください (Assert.Empty を使う)
        Assert.Fail("TODO: Assert.Empty を使って検証してください");
    }

    [Fact]
    public void カートをクリアすると合計金額が0になる()
    {
        // Arrange
        var cart = new ShoppingCartService();
        cart.AddItem("りんご", 100m, 3);
        cart.AddItem("みかん", 50m, 2);

        // Act
        cart.Clear();

        // Assert
        // 穴埋め: cart.TotalAmount が 0 であることを検証してください
        Assert.Fail("TODO: cart.TotalAmount を検証してください");
    }
}
