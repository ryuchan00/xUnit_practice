using TrainingApp.Tests.Infrastructure;

namespace TrainingApp.Tests.Level6_OrderingAndParallelization;

// ===================================================================
// Level 6-B: テストの並列化とコレクション
// -------------------------------------------------------------------
// xUnit はデフォルトで「異なるテストクラス」を並列に実行します
// (同じクラス内のテストは直列に実行されます)。
//
// 共有状態(静的フィクスチャ、DB、外部ファイルなど)を扱うテストクラスを
// うっかり並列実行してしまうと、テスト結果が不安定になります。
//
// [Collection(SharedCounterCollection.Name)] を同じ名前で複数のクラスに
// 付けると、それらのクラス同士は並列実行されなくなり、
// 同じ SharedCounterFixture インスタンスを安全に共有できます。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level6_OrderingAndParallelization"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level6_OrderingAndParallelization"
// ===================================================================
[Collection(SharedCounterCollection.Name)]
public class ParallelCollectionExampleTestsA
{
    private readonly SharedCounterFixture _fixture;

    public ParallelCollectionExampleTestsA(SharedCounterFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void 共有カウンタをインクリメントするとき値が1増える()
    {
        // Arrange
        var before = _fixture.Current;

        // Act
        _fixture.Increment();

        // Assert
        // 穴埋め: _fixture.Current が before + 1 であることを検証してください
        Assert.Fail("TODO: _fixture.Current を検証してください");
    }
}

[Collection(SharedCounterCollection.Name)]
public class ParallelCollectionExampleTestsB
{
    private readonly SharedCounterFixture _fixture;

    public ParallelCollectionExampleTestsB(SharedCounterFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void 共有カウンタをインクリメントするとき値が1増える()
    {
        // Arrange
        var before = _fixture.Current;

        // Act
        _fixture.Increment();

        // Assert
        // 穴埋め: _fixture.Current が before + 1 であることを検証してください
        Assert.Fail("TODO: _fixture.Current を検証してください");
    }
}
