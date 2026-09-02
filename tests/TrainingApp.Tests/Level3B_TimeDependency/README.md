# Level 3B: 時間に依存するコードのテスト

## これから何をするか

`DateTime.Now` を直接使わず `TimeProvider` を注入する設計にし、テスト側では `FakeTimeProvider` で時刻を固定してから検証します。

## なぜ時間に依存するテストを気を付けるか

現在時刻に依存するコードをそのままテストすると、実行するタイミングによって結果が変わってしまう不安定なテスト(flaky test)になります。時刻を固定できる設計にすることで、いつ実行しても同じ結果になります。

## サンプルコード

```csharp
[Fact]
public void IsWeekend_土曜日なら_trueを返す()
{
    // Arrange
    var fakeTime = new FakeTimeProvider();
    fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 8, 0, 0, 0, TimeSpan.Zero)); // 土曜日
    var calendar = new BusinessCalendar(fakeTime);

    // Act
    var actual = calendar.IsWeekend();

    // Assert
    Assert.True(actual);
}
```
