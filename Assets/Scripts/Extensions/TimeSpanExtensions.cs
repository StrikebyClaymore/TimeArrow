using System;

namespace Extensions
{
    public static class TimeSpanExtensions
    {
        public static string FormatToAlarmTime(this TimeSpan timeSpan)
        {
            var time = new TimeSpan(AlarmClockManager.AlarmClock.Hour, AlarmClockManager.AlarmClock.Minute, 0);
            return time.ToString("hh':'mm");
        }
    }
}
