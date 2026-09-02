using TrainingApp.Services;

namespace TrainingApp.Tests.Level3_StateChange;

// ===================================================================
// Level 3: Act した後に状態が変化することを確認する
// -------------------------------------------------------------------
// これまでは戻り値を検証してきましたが、ここでは Act の実行によって
// オブジェクトの「状態」がどう変わるかを検証します。
//
// 【このLevelからテストは自分たちで書きます】
// - 最初の1本だけ、完成したテストが「見本」として書いてあります。
// - 残りは Skip 付きの空メソッドです。Skip を外し、Arrange/Act/Assert を
//   自分たちで書いてください(見本と同じ3Aの型で書けます)。
// - プロダクションコード(ShoppingCartService.cs)は変更しないでください。
//
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level3_StateChange"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level3_StateChange"
// ===================================================================
public class ShoppingCartServiceTests
{
    // --- AddItem() のテスト ---
    public class AddItem
    {
        [Fact]
        public void 商品を1つ追加するとき合計金額が加算される()
        {
            // Arrange

            // Act

            // Assert
            // 期待値は計算式(100m * 3)ではなく、値そのものを書くこと。
            // 計算式を書くと、プロダクションコードと同じ間違いをテスト側でもしてしまい、意図しない退行が防げなくなる。
        }

        // --- ここから先は自分たちで書く ---

        [Fact(Skip = "要件: 商品を2種類追加すると、合計個数(TotalItemCount)が集計される")]
        public void 商品を2種類追加するとき合計個数が集計される()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    // --- RemoveItem() のテスト ---
    public class RemoveItem
    {
        [Fact(Skip = "要件: 追加した商品を削除すると、一覧(Items)から消える。ヒント: Assert.Empty")]
        public void 追加した商品を削除するとき一覧から消える()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    // --- Clear() のテスト ---
    public class Clear
    {
        [Fact(Skip = "要件: カートをクリアすると、合計金額が 0 になる")]
        public void カートをクリアするとき合計金額が0になる()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    // --- 発展課題 ---
    // ここから下は、テストメソッド名すら決まっていません。
    // README.md の「発展課題」にある要件を読み、「どうあるべきか」を決め、
    // テストの名前を考えるところから始めてください。
}
