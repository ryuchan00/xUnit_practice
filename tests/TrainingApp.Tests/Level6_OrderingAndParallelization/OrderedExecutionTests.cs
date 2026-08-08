using TrainingApp.Tests.Infrastructure;

namespace TrainingApp.Tests.Level6_OrderingAndParallelization;

// ===================================================================
// Level 6-A: カスタム属性によるテスト実行順序の指定
// -------------------------------------------------------------------
// xUnit は本来「各テストは独立していて、どの順序で実行されても良い」
// ことを前提に設計されています。そのため、通常は実行順序に依存する
// テストを書くべきではありません。
//
// ただし、シナリオテストなど「順序に意味がある」ケースも実務では
// 存在します。ここでは [TestCaseOrderer] + [TestPriority] を使って
// 実行順序を制御する方法を学びます。
//
// 各テストは、自分が実行された時点での ExecutionLog の件数を検証しています。
// つまり Step1 → Step2 → Step3 の順で実行されない限り、いずれかのテストが
// 必ず失敗します。3つのテストすべてに正しい [TestPriority] を付けて、
// Step1 → Step2 → Step3 の順に実行されるようにしてください。
// (このクラスはメソッドをあえて Step3, Step1, Step2 の順に定義しています)
// ===================================================================
[TestCaseOrderer("TrainingApp.Tests.Infrastructure.PriorityOrderer", "TrainingApp.Tests")]
public class OrderedExecutionTests
{
    private static readonly List<string> ExecutionLog = [];

    [Fact]
    // 穴埋め: [TestPriority(?)] を付けて、このテストが3番目に実行されるようにしてください
    public void Step3_3番目に実行されるべきテスト()
    {
        Assert.True(ExecutionLog.Count == 2, $"想定と異なる実行順です(現在の件数: {ExecutionLog.Count})");
        ExecutionLog.Add(nameof(Step3_3番目に実行されるべきテスト));
    }

    [Fact]
    // 穴埋め: [TestPriority(?)] を付けて、このテストが最初に実行されるようにしてください
    public void Step1_最初に実行されるべきテスト()
    {
        Assert.True(ExecutionLog.Count == 0, $"想定と異なる実行順です(現在の件数: {ExecutionLog.Count})");
        ExecutionLog.Add(nameof(Step1_最初に実行されるべきテスト));
    }

    [Fact]
    // 穴埋め: [TestPriority(?)] を付けて、このテストが2番目に実行されるようにしてください
    public void Step2_2番目に実行されるべきテスト()
    {
        Assert.True(ExecutionLog.Count == 1, $"想定と異なる実行順です(現在の件数: {ExecutionLog.Count})");
        ExecutionLog.Add(nameof(Step2_2番目に実行されるべきテスト));
    }
}
