namespace Sample.Architecture.Extensions.Application.Common.Abstractions.Utilities;
public interface ITimeUtility
{
    TimeZoneInfo GetTimeZoneInfo();

    DateTimeOffset GetUtcDateTimeOffset();
    DateTimeOffset GetLocalDateTimeOffset();

    DateTime GetUtcDateTime();
    DateTime GetLocalDateTime();

    DateOnly GetUtcDateOnly();
    DateOnly GetLocalDateOnly();

    TimeOnly GetUtcTimeOnly();
    TimeOnly GetLocalTimeOnly();

    long GetUtcTimestampInMilliseconds();
    long GetLocalTimestampInMilliseconds();

    long GetUtcTimestampInSeconds();
    long GetLocalTimestampInSeconds();
}