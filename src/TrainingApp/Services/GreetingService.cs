namespace TrainingApp.Services;

/// <summary>
/// 現在時刻に依存するロジックを持つサービス。
/// DateTime.Now / DateTimeOffset.UtcNow を直接呼び出さず、TimeProvider を
/// コンストラクタで受け取ることで、テスト側から時刻を差し替えられるようにしている。
/// </summary>
public class GreetingService
{
    private readonly TimeProvider _timeProvider;

    public GreetingService(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    /// <summary>現在時刻(UTC)の時間帯に応じた挨拶を返す。</summary>
    public string GetGreeting()
    {
        var hour = _timeProvider.GetUtcNow().Hour;
        return hour switch
        {
            >= 5 and < 12 => "おはようございます",
            >= 12 and < 18 => "こんにちは",
            _ => "こんばんは",
        };
    }

    /// <summary>平日(月〜金)の 9:00〜18:00(UTC) かどうかを判定する。</summary>
    public bool IsWithinBusinessHours()
    {
        var now = _timeProvider.GetUtcNow();
        if (now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return false;
        }

        return now.Hour is >= 9 and < 18;
    }

    /// <summary>指定した有効期限を現在時刻(UTC)が過ぎているかどうかを判定する。</summary>
    public bool IsExpired(DateTimeOffset expiresAt)
    {
        return _timeProvider.GetUtcNow() > expiresAt;
    }
}
