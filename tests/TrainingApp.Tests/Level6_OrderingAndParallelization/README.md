# Level 6: テストの実行順序と並列化

## これから何をするか

カスタム属性でテストの実行順序を制御する方法と、`[Collection]` で共有状態を持つテストクラス同士の並列実行を防ぐ方法を学びます。

## なぜそうするか

xUnit のテストは本来、どの順序で実行されても良いように独立して書くべきです。ただし、シナリオテストや共有リソースを扱うテストでは、順序や並列度を意図的に制御しないと結果が不安定になります。

## サンプルコード

```csharp
[TestCaseOrderer("MyApp.Tests.Infrastructure.PriorityOrderer", "MyApp.Tests")]
public class LoginScenarioTests
{
    [Fact]
    [TestPriority(1)]
    public void Step1_ユーザー登録が成功する()
    {
        // ...
    }

    [Fact]
    [TestPriority(2)]
    public void Step2_登録したユーザーでログインできる()
    {
        // ...
    }
}
```
