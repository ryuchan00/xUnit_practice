using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level2_AssertTypes;

// ===================================================================
// Level 2: いろいろな Assert の種類
// -------------------------------------------------------------------
// xUnit には Assert.Equal 以外にも多くの検証メソッドがあります。
// 場面に応じて適切な Assert メソッドを選んで穴埋めしてください。
//
// DiscountService は「利用可能なクーポンの一覧」をコンストラクタで受け取ります。
// どのクーポンが存在するかはテスト側が決めるので、Arrange ではそのテストに
// 必要なクーポンだけを渡してください(関係ないテストでは空の [] でよい)。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level2_AssertTypes"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level2_AssertTypes"
// ===================================================================
public class DiscountServiceTests
{
    // --- IsEligibleForFreeShipping() のテスト ---
    public class IsEligibleForFreeShipping
    {
        [Fact]
        public void 購入金額が10000円以上のとき送料無料の対象になる()
        {
            // Arrange
            // このテストにクーポンは関係ないので、空の一覧を渡しておけばよい。
            var service = new DiscountService([]);

            // Act

            // Assert
        }

        [Fact]
        public void 購入金額が10000円未満のとき送料無料の対象にならない()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    // --- FindCoupon() のテスト ---
    public class FindCoupon
    {
        [Fact]
        public void 存在するクーポンコードを渡すときクーポンが見つかる()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void 存在しないクーポンコードを渡すときクーポンが見つからない()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    // --- GetAvailableCouponCodes() のテスト ---
    public class GetAvailableCouponCodes
    {
        [Fact]
        public void 利用可能なクーポンコードが3件あるとき3件取得できる()
        {
            // Arrange       

            // Act

            // Assert
 
        }
    }
}
