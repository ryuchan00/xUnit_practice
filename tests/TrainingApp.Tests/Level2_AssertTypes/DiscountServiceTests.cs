using TrainingApp.Services;

namespace TrainingApp.Tests.Level2_AssertTypes;

// ===================================================================
// Level 2: いろいろな Assert の種類
// -------------------------------------------------------------------
// xUnit には Assert.Equal 以外にも多くの検証メソッドがあります。
// 場面に応じて適切な Assert メソッドを選んで穴埋めしてください。
// ===================================================================
public class DiscountServiceTests
{
    [Fact]
    public void IsEligibleForFreeShipping_10000円以上なら_trueを返す()
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
    public void IsEligibleForFreeShipping_10000円未満なら_falseを返す()
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
    public void FindCoupon_存在するコードを渡すと_nullではないクーポンを返す()
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
    public void FindCoupon_存在しないコードを渡すと_nullを返す()
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
    public void GetAvailableCouponCodes_3件のクーポンコードを含む()
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
