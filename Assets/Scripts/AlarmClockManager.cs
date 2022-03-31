using System;
using UnityEngine;

[RequireComponent(typeof(AlarmPlayer))]
public class AlarmClockManager : MonoBehaviour
{
    public static AlarmClock AlarmClock;
    private AlarmPlayer _alarmPlayer;
    public int dayOfWeek;

    private void Awake()
    {
        _alarmPlayer = GetComponent<AlarmPlayer>();
        AlarmClock = new AlarmClock();
        Load();
    }

    public void StartAlarm()
    {
        ApplicationManager.RootMenu.clockMenu.OpenAlarmSubMenu();
        _alarmPlayer.Play();
        AlarmClock.CurrentDayAlarmSetPlayed();
    }

    public void StopAlarm()
    {
        _alarmPlayer.Stop();
    }

    public void PostponeAlarm()
    {
        AlarmClock.Minute += 5;
        ApplicationManager.RootMenu.clockMenu.UpdateAlarmClock();
    }
    
    public bool TimeIsAlarmTime()
    {
        if (!AlarmClock.On || AlarmClock.CurrentDayAlarmIsPlayed())
            return false;
        var systemTime = DateTime.Now;
        var alarmTime = AlarmClock.GetDateTime();
        return systemTime.Hour == alarmTime.Hour && systemTime.Minute == alarmTime.Minute;
    }

    public void CheckNextWeek() // TODO: rewrite this method for fix bug
    {
        var systemDayOfWeek = (int) DateTime.Now.DayOfWeek - 1;
        if (dayOfWeek == systemDayOfWeek)
            return;
        if (dayOfWeek == 6 && systemDayOfWeek == 0)
        {
            AlarmClock.ResetAllDays();
        }
        dayOfWeek = systemDayOfWeek;
    }

    public void SetAlarm()
    {
        AlarmClock.Hour = DateTime.Now.Hour;
        AlarmClock.Minute = DateTime.Now.Minute;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("DayOfWeek", dayOfWeek + 1);
    }
    
    private void Load()
    {
        dayOfWeek = PlayerPrefs.GetInt("DayOfWeek");
        if (dayOfWeek == 0)
            dayOfWeek = (int) DateTime.Now.DayOfWeek;
        dayOfWeek--;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
