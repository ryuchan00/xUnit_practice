using Microsoft.AspNetCore.Mvc;
using Moq;
using TrainingApp.Controllers;
using TrainingApp.Models;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level4_ControllerSpec;

// ===================================================================
// Level 4: Controller のテスト (Controller Spec)
// -------------------------------------------------------------------
// Controller は「HTTP のリクエスト/レスポンス」を扱う層です。
// ここでは実際のリポジトリ実装は使わず、Moq を使って IProductRepository を
// 「モック」に差し替え、Controller の振る舞い(ステータスコードや戻り値)だけを
// 検証します。 Moq の詳しい使い方は Level 5 で学びます。
// ===================================================================
public class ProductsControllerTests
{
    [Fact]
    public void GetById_存在するIdを渡すと_Okとその商品を返す()
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
    public void GetById_存在しないIdを渡すと_NotFoundを返す()
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

    [Fact]
    public void Create_商品を渡すと_CreatedAtActionを返す()
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
