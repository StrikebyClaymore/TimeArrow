﻿using System;

public class AlarmClock
{
    public bool Created = false;
    public bool On { get; private set; }
    public int Hour;
    public int Minute;
    public int NewHour;
    public int NewMinute;
    public bool[] DaysOn = new bool[7];
    private bool[] _playedAlarmsList = new bool[7];

    public void SetDay(int idx)
    {
        DaysOn[idx] = !DaysOn[idx];
    }

    public void SetNewTime()
    {
        Hour = NewHour;
        Minute = NewMinute;
    }
    
    public void ResetAllDays() => _playedAlarmsList = new bool[7];
    
    public void ResetCurrentDay() => _playedAlarmsList[ApplicationManager.AlarmClockManager.weekOfMonth] = false;
    
    public void Enable() => On = !On; //  && DaysOn.FirstOrDefault(day => day)

    public bool CurrentDayAlarmIsPlayed() => _playedAlarmsList[ApplicationManager.AlarmClockManager.weekOfMonth];

    public void CurrentDayAlarmSetPlayed() => _playedAlarmsList[ApplicationManager.AlarmClockManager.weekOfMonth] = true;

    public DateTime GetDateTime()
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

        var currentDate = Clock.ClockTime;
        var alarmDateTime = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
            Hour, Minute, 0);

        var dayDifference = dayOfWeek - (int)alarmDateTime.DayOfWeek;
        alarmDateTime = alarmDateTime.AddDays(dayDifference);

        return alarmDateTime;
    }
}
