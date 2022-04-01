using System;

namespace Extensions
{
    public static class DateTimeExtensions
    {
        public static int WeekOfMonth(this DateTime date)
        {
            var month = date.Month;
            var startDate = new DateTime(2022, month, 1);
            var firstDay = (int) startDate.DayOfWeek;
            var diff = 8 - firstDay;
            var currentWeek = (int) Math.Ceiling((double) (date.Day + (7 - diff)) / 7);
            return currentWeek;
        }
    }
}