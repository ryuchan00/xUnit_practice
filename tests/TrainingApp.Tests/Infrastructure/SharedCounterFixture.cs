namespace TrainingApp.Tests.Infrastructure;

/// <summary>
/// 複数のテストクラスから共有される「グローバルな状態」を模したフィクスチャ。
/// 本来テストは独立しているべきだが、静的なリソースやDBなど
/// 「共有状態」を扱うテストは並列実行すると値が競合し、
/// 結果が不安定(flaky)になることがある。
/// </summary>
public class SharedCounterFixture
{
    private int _counter;

    public int Increment() => Interlocked.Increment(ref _counter);

    public int Current => _counter;
}

/// <summary>
/// このコレクションに属するテストクラス同士は、xUnit によって
/// 「並列実行されない(順番に実行される)」ことが保証される。
/// (別のコレクションのテストとは並列に実行され得る)
/// </summary>
[CollectionDefinition(Name)]
public class SharedCounterCollection : ICollectionFixture<SharedCounterFixture>
{
    public const string Name = "Shared Counter Collection";
}
