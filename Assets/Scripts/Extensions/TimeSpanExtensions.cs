using System;

namespace Extensions
{
    public static class TimeSpanExtensions
    {
        public static string FormatToAlarmTime(this TimeSpan timeSpan, bool newTime = false)
        {
            var time = new TimeSpan(AlarmClockManager.AlarmClock.Hour, AlarmClockManager.AlarmClock.Minute, 0);
            if(newTime)
                time = new TimeSpan(AlarmClockManager.AlarmClock.NewHour, AlarmClockManager.AlarmClock.NewMinute, 0);
            return time.ToString("hh':'mm");
        }
    }
}
