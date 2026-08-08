using TrainingApp.Services;

namespace TrainingApp.Tests.Level2_AssertTypes;

// ===================================================================
// Level 2: いろいろな Assert の種類
// -------------------------------------------------------------------
// xUnit には Assert.Equal 以外にも多くの検証メソッドがあります。
// 場面に応じて適切な Assert メソッドを選んで穴埋めしてください。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level2_AssertTypes"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level2_AssertTypes"
// ===================================================================
public class DiscountServiceTests
{
    [Fact]
    public void 購入金額が10000円以上のとき送料無料の対象になる()
    {
        // Arrange
        var service = new DiscountService();

        // Act
        var actual = service.IsEligibleForFreeShipping(10000m);

        // Assert
        // 穴埋め: actual が true であることを検証してください (Assert.True を使う)
        Assert.Fail("TODO: Assert.True を使って検証してください");
    }

    [Fact]
    public void 購入金額が10000円未満のとき送料無料の対象にならない()
    {
        // Arrange
        var service = new DiscountService();

        // Act
        var actual = service.IsEligibleForFreeShipping(9999m);

        // Assert
        // 穴埋め: actual が false であることを検証してください (Assert.False を使う)
        Assert.Fail("TODO: Assert.False を使って検証してください");
    }

    [Fact]
    public void 存在するクーポンコードを渡すときクーポンが見つかる()
    {
        // Arrange
        var service = new DiscountService();

        // Act
        var actual = service.FindCoupon("SAVE10");

        // Assert
        // 穴埋め: actual が null でないことを検証してください (Assert.NotNull を使う)
        Assert.Fail("TODO: Assert.NotNull を使って検証してください");
    }

    [Fact]
    public void 存在しないクーポンコードを渡すときクーポンが見つからない()
    {
        // Arrange
        var service = new DiscountService();

        // Act
        var actual = service.FindCoupon("NOT_EXIST");

        // Assert
        // 穴埋め: actual が null であることを検証してください (Assert.Null を使う)
        Assert.Fail("TODO: Assert.Null を使って検証してください");
    }

    [Fact]
    public void 利用可能なクーポンコードが3件取得できる()
    {
        // Arrange
        var service = new DiscountService();

        // Act
        var actual = service.GetAvailableCouponCodes();

        // Assert
        // 穴埋め1: actual の件数が 3 件であることを検証してください (Assert.Equal(3, actual.Count) など)
        // 穴埋め2: actual に "VIP30" が含まれることを検証してください (Assert.Contains を使う)
        Assert.Fail("TODO: Assert.Equal と Assert.Contains を使って検証してください");
    }
}
