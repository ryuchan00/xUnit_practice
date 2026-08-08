using TrainingApp.Services;

namespace TrainingApp.Tests.Level3_StateChange;

// ===================================================================
// Level 3: Act した後に状態が変化することを確認する
// -------------------------------------------------------------------
// これまでは戻り値を検証してきましたが、ここでは Act の実行によって
// オブジェクトの「状態」がどう変わるかを検証します。
// ===================================================================
public class ShoppingCartServiceTests
{
    [Fact]
    public void AddItem_商品を1つ追加すると_TotalAmountが加算される()
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
    public void AddItem_商品を2つ追加すると_TotalItemCountが合計数になる()
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
    public void RemoveItem_追加した商品を削除すると_Itemsから消える()
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
    public void Clear_複数商品を追加後にクリアすると_TotalAmountが0になる()
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
