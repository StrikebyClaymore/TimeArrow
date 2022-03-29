using System;
using UnityEngine;

public class AlarmClockManager : MonoBehaviour
{
    public static AlarmClock AlarmClock;

    private void Awake()
    {
        AlarmClock = new AlarmClock();
    }

    public void SetAlarmClockActive()
    {
        AlarmClock.On = !AlarmClock.On;
    }
    
    public string FormatTime()
    {
        var timeSpan = new TimeSpan(AlarmClock.Hour, AlarmClock.Minute, 0);
        return timeSpan.ToString("hh':'mm");
    }
    
}
