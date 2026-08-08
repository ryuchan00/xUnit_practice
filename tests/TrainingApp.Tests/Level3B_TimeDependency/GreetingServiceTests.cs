using Microsoft.Extensions.Time.Testing;
using TrainingApp.Services;

namespace TrainingApp.Tests.Level3B_TimeDependency;

// ===================================================================
// Level 3B: 時間に依存するコードのテスト
// -------------------------------------------------------------------
// もし GreetingService が内部で DateTime.Now や DateTimeOffset.UtcNow を
// 直接呼び出していたら、そのテストは「実行した時刻」によって結果が変わって
// しまいます。朝に実行すれば通るが、夜に実行すると落ちる…という
// 典型的な flaky test(不安定なテスト)になります。
//
// GreetingService はコンストラクタで TimeProvider を受け取るように
// 作られているため、テスト側では本物の時計の代わりに FakeTimeProvider を
// 渡し、時刻を固定(freeze)してから検証します。これにより、
// 「いつ実行しても同じ結果になる」決定的なテストになります。
//
// FakeTimeProvider を使わずに実行すると、GreetingService は
// TimeProvider を要求するコンストラクタしか持たないため、
// そもそもコンパイルが通りません。つまりこのテストは
// 「時刻を固定しないとテストを書けない」設計になっています。
// 実行方法(このLevelだけ実行する場合、リポジトリルートで実行):
//   dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level3B_TimeDependency"
// Docker で実行する場合:
//   docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level3B_TimeDependency"
// ===================================================================
public class GreetingServiceTests
{
    [Fact]
    public void 朝7時のとき_おはようございますが返る()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 10, 7, 0, 0, TimeSpan.Zero)); // 2026-08-10(月) 07:00 UTC
        var service = new GreetingService(fakeTime);

        // Act
        var actual = service.GetGreeting();

        // Assert
        // 穴埋め: actual が "おはようございます" であることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }

    [Fact]
    public void 昼14時のとき_こんにちはが返る()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 10, 14, 0, 0, TimeSpan.Zero)); // 2026-08-10(月) 14:00 UTC
        var service = new GreetingService(fakeTime);

        // Act
        var actual = service.GetGreeting();

        // Assert
        // 穴埋め: actual が "こんにちは" であることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }

    [Fact]
    public void 夜20時のとき_こんばんはが返る()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 10, 20, 0, 0, TimeSpan.Zero)); // 2026-08-10(月) 20:00 UTC
        var service = new GreetingService(fakeTime);

        // Act
        var actual = service.GetGreeting();

        // Assert
        // 穴埋め: actual が "こんばんは" であることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }

    [Fact]
    public void 平日10時のとき営業時間内と判定される()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 10, 10, 0, 0, TimeSpan.Zero)); // 2026-08-10(月) 10:00 UTC
        var service = new GreetingService(fakeTime);

        // Act
        var actual = service.IsWithinBusinessHours();

        // Assert
        // 穴埋め: actual が true であることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }

    [Fact]
    public void 土曜10時のとき営業時間外と判定される()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 8, 10, 0, 0, TimeSpan.Zero)); // 2026-08-08(土) 10:00 UTC
        var service = new GreetingService(fakeTime);

        // Act
        var actual = service.IsWithinBusinessHours();

        // Assert
        // 穴埋め: 時刻は営業時間内(10時)でも、曜日が土曜日なので false になることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }

    [Fact]
    public void 有効期限が過去の日時のとき期限切れと判定される()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 10, 0, 0, 0, TimeSpan.Zero));
        var service = new GreetingService(fakeTime);
        var expiresAt = new DateTimeOffset(2026, 8, 1, 0, 0, 0, TimeSpan.Zero); // 現在より過去

        // Act
        var actual = service.IsExpired(expiresAt);

        // Assert
        // 穴埋め: actual が true であることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }

    [Fact]
    public void 有効期限が未来の日時のときまだ有効と判定される()
    {
        // Arrange
        var fakeTime = new FakeTimeProvider();
        fakeTime.SetUtcNow(new DateTimeOffset(2026, 8, 10, 0, 0, 0, TimeSpan.Zero));
        var service = new GreetingService(fakeTime);
        var expiresAt = new DateTimeOffset(2026, 9, 1, 0, 0, 0, TimeSpan.Zero); // 現在より未来

        // Act
        var actual = service.IsExpired(expiresAt);

        // Assert
        // 穴埋め: actual が false であることを検証してください
        Assert.Fail("TODO: actual を検証してください");
    }
}
