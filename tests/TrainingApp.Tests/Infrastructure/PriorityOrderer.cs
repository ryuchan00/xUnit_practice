using Xunit.Abstractions;
using Xunit.Sdk;

namespace TrainingApp.Tests.Infrastructure;

/// <summary>
/// [TestPriority] 属性の値をもとにテストケースを並び替える ITestCaseOrderer。
/// テストクラスに [TestCaseOrderer(...)] を付けることで有効になる。
/// </summary>
public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var sortedByPriority = new SortedDictionary<int, List<TTestCase>>();

        foreach (var testCase in testCases)
        {
            var priority = testCase.TestMethod.Method
                .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName)
                .FirstOrDefault()
                ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

            if (!sortedByPriority.TryGetValue(priority, out var list))
            {
                list = [];
                sortedByPriority[priority] = list;
            }

            list.Add(testCase);
        }

        foreach (var list in sortedByPriority.Values)
        {
            foreach (var testCase in list)
            {
                yield return testCase;
            }
        }
    }
}
