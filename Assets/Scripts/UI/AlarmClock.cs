using System;
using System.Linq;
using UnityEngine;

public class AlarmClock
{
    public bool Created = false;
    public bool On;
    public int Hour;
    public int Minute;
    public bool[] DaysOn = new bool[7];

    public void SetDay(int idx)
    {
        DaysOn[idx] = !DaysOn[idx];
    }
    
    public void Reset()
    {
        Hour = 12;
        Minute = 0;
    }

    public DateTime GetTime()
    {
        var dayOfWeek = 1;
        while (dayOfWeek < 7)
        {
            if (DaysOn[dayOfWeek])
            {
                break;
            }
            dayOfWeek++;
        }

        var currentDate = DateTime.Now;
        var alarmDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
            Hour, Minute, 0);

        var dayDifference = dayOfWeek - (int)alarmDate.DayOfWeek;
        alarmDate = alarmDate.AddDays(dayDifference);

        Debug.Log(alarmDate.ToString("u"));
        
        return alarmDate;
    }
}
