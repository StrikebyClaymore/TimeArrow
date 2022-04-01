using System;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(AlarmPlayer))]
public class AlarmClockManager : MonoBehaviour
{
    public static AlarmClock AlarmClock;
    private AlarmPlayer _alarmPlayer;
    public int weekOfMonth;
    private DateTime _date;

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

    public void CheckNextWeek()
    {
        var systemDate = DateTime.Now;
        var systemWeekOfMonth = systemDate.WeekOfMonth();
        if (_date.Year == systemDate.Year && _date.Month == systemDate.Month && weekOfMonth == systemWeekOfMonth)
            return;
        _date = systemDate;
        weekOfMonth = systemWeekOfMonth;
        AlarmClock.ResetAllDays();
    }

    public void SetAlarm()
    {
        AlarmClock.Hour = DateTime.Now.Hour;
        AlarmClock.Minute = DateTime.Now.Minute;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("WeekOfMonth", weekOfMonth);
        PlayerPrefs.SetString("Date", _date.ToString("d"));
    }
    
    private void Load()
    {
        weekOfMonth = PlayerPrefs.GetInt("WeekOfMonth");
        if (weekOfMonth == 0)
            weekOfMonth = DateTime.Now.WeekOfMonth();
        _date = DateTime.Parse(PlayerPrefs.GetString("Date"));
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
