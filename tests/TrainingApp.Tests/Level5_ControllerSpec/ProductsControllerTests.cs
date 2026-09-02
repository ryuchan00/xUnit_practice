using Microsoft.AspNetCore.Mvc;
using Moq;
using TrainingApp.Controllers;
using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level5_ControllerSpec;

// ===================================================================
// Level 5: Controller のテスト (Controller Spec)
// -------------------------------------------------------------------
// Controller は「HTTP のリクエスト/レスポンス」を扱う層です。
// ここでは実際のリポジトリ実装は使わず、Moq を使って IProductRepository を
// 「モック」に差し替え、Controller の振る舞い(ステータスコードや戻り値)だけを
// 検証します。 Moq の使い方は Level 4 で学んだとおりです。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_ControllerSpec"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level5_ControllerSpec"
// ===================================================================
public class ProductsControllerTests
{
    // --- GetById() のテスト ---
    public class GetById
    {
        [Fact]
        public void 存在するIdを指定するとき商品が200OKで返る()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var expected = new Product(1, "テスト商品", 1000m);
            mockRepository.Setup(r => r.GetById(1)).Returns(expected);
            var controller = new ProductsController(mockRepository.Object);

            // Act
            var actual = controller.GetById(1);

            // Assert
            // 穴埋め1: actual.Result が OkObjectResult 型であることを検証してください
            //          (Assert.IsType<OkObjectResult>(actual.Result) を使う)
            // 穴埋め2: OkObjectResult.Value が expected と一致することを検証してください
            Assert.Fail("TODO: OkObjectResult 型であることと Value を検証してください");
        }

        [Fact]
        public void 存在しないIdを指定するとき404NotFoundが返る()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns((Product?)null);
            var controller = new ProductsController(mockRepository.Object);

            // Act
            var actual = controller.GetById(999);

            // Assert
            // 穴埋め: actual.Result が NotFoundResult 型であることを検証してください
            Assert.Fail("TODO: NotFoundResult 型であることを検証してください");
        }
    }

    // --- Create() のテスト ---
    public class Create
    {
        [Fact]
        public void 商品を登録するとき201CreatedAtActionが返る()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var input = new Product(0, "新商品", 500m);
            var saved = input with { Id = 42 };
            mockRepository.Setup(r => r.Add(input)).Returns(saved);
            var controller = new ProductsController(mockRepository.Object);

            // Act
            var actual = controller.Create(input);

            // Assert
            // 穴埋め1: actual.Result が CreatedAtActionResult 型であることを検証してください
            // 穴埋め2: リポジトリの Add(input) がちょうど1回呼ばれたことを検証してください
            //          (mockRepository.Verify(r => r.Add(input), Times.Once) を使う)
            Assert.Fail("TODO: CreatedAtActionResult 型であることと Add の呼び出しを検証してください");
        }
    }
}
