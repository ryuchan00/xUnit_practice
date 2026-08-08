namespace TrainingApp.Tests.Infrastructure;

/// <summary>
/// テストの実行順序を指定するカスタム属性。
/// 数値が小さいほど先に実行される。
/// 通常、xUnit のテスト実行順序は保証されないため、順序に依存するテストを
/// 書きたい場合は、この属性 + PriorityOrderer を使う。
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute(int priority) : Attribute
{
    public int Priority { get; } = priority;
}
