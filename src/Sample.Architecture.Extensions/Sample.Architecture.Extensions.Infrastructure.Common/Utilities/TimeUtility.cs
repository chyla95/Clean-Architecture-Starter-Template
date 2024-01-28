using Sample.Architecture.Extensions.Application.Common.Abstractions.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Common.Utilities;
internal sealed class TimeUtility : ITimeUtility
{
    public TimeZoneInfo GetTimeZoneInfo() => TimeZoneInfo.Local;

    public DateTimeOffset GetUtcDateTimeOffset() => DateTimeOffset.UtcNow;
    public DateTimeOffset GetLocalDateTimeOffset() => DateTimeOffset.Now;

    public DateTime GetUtcDateTime() => DateTimeOffset.UtcNow.DateTime;
    public DateTime GetLocalDateTime() => DateTimeOffset.Now.DateTime;

    public DateOnly GetUtcDateOnly() => DateOnly.FromDateTime(DateTime.UtcNow);
    public DateOnly GetLocalDateOnly() => DateOnly.FromDateTime(DateTime.Now);

    public TimeOnly GetUtcTimeOnly() => TimeOnly.FromDateTime(DateTime.UtcNow);
    public TimeOnly GetLocalTimeOnly() => TimeOnly.FromDateTime(DateTime.Now);

    public long GetUtcTimestampInMilliseconds() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long GetLocalTimestampInMilliseconds() => DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public long GetUtcTimestampInSeconds() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long GetLocalTimestampInSeconds() => DateTimeOffset.Now.ToUnixTimeSeconds();
}